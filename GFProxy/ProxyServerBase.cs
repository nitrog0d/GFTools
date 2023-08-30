using System.Net.Sockets;

namespace GFProxy;
public abstract class ProxyServerBaseImpl {
    public Dictionary<ushort, (ICommandHandler, Type)> Handlers = new();
}

public abstract class ProxyServerBase<T> : ProxyServerBaseImpl where T : ProxyClientBase, new() {
    public Logger Logger { get; private set; }

    public ProxyServerBase(int proxyPort, string serverIp, int serverPort) {
        var serverType = GetType().Name;
        Logger = new(serverType);

        foreach (var handler in GetType().Module.GetTypes().Where(t => t.Namespace == $"GFProxy.Handlers.{serverType}")) {
            var commandType = handler.BaseType.GetGenericArguments()[1];

            var commandId = (ushort)commandType.GetField("CommandID").GetRawConstantValue();

            Logger.VerboseLine($"Registered handler for {commandType.Name}");

            Handlers.Add(commandId, ((ICommandHandler)Activator.CreateInstance(handler), commandType));
        }

        var proxyServer = new TcpListener(System.Net.IPAddress.Any, proxyPort);
        proxyServer.Start();

        Logger.InfoLine($"Listening on port {proxyPort}");

        var proxyServerThread = new Thread(() => {
            while (true) {
                // This blocks the thread until we get a connection
                var clientSocket = proxyServer.AcceptTcpClient();

                var clientLogger = new Logger(Logger.Source);

                clientLogger.VerboseLine($"Client connected: {clientSocket.Client.RemoteEndPoint}");

                var serverSocket = new TcpClient(serverIp, serverPort);

                Logger.VerboseLine($"Connected client to server!");

                clientLogger.Source = $"{Logger.Source} - {clientSocket.Client.RemoteEndPoint}";

                var proxyClient = new T() {
                    Context = this,
                    Logger = clientLogger,
                    ClientSocket = clientSocket,
                    ServerSocket = serverSocket
                };
                proxyClient.Init();
            }
        });

        proxyServerThread.Name = $"{serverType} Thread";
        proxyServerThread.Start();
    }
}
