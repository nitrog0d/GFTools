using GFTools.Common.Protocol;
using System.Net.Sockets;

namespace CGITool;

public class Program {
    public const string CGIKey = "0KjaM85BjfqjA";

    public const string ZoneServerIP = "51.222.85.115";
    public const int CGIPort = 20061;

    public static TcpClient ServerSocket { get; set; }

    public static void Main(string[] args) {
        Console.Title = "GFCGI";

        Console.WriteLine("Connecting to ZoneServer CGI...");

        ServerSocket = new TcpClient(ZoneServerIP, CGIPort);

        var serverThread = new Thread(HandleServerSocket);
        serverThread.Name = $"Server Socket Thread";
        serverThread.Start();

        Console.WriteLine("Connected!");

        ReceiveConsoleInput();
    }

    public static void ReceiveConsoleInput() {
        while (true) {
            var input = Console.ReadLine();

            var memoryStream = new MemoryStream();
            var binaryWriter = new GFBinaryWriter(memoryStream);

            binaryWriter.WriteString($"{CGIKey},{input}");

            var packet = CreatePacket(memoryStream.ToArray());

            ServerSocket.GetStream().Write(packet);
        }
    }

    public static byte[] CreatePacket(byte[] data) {
        var packetSize = (ushort)data.Length;
        var packet = new byte[sizeof(ushort) + packetSize];

        Buffer.BlockCopy(BitConverter.GetBytes(packetSize), 0, packet, 0, sizeof(ushort));
        Buffer.BlockCopy(data, 0, packet, sizeof(ushort), packetSize);

        return packet;
    }

    private static void HandleServerRawPacket(byte[] data) {
        var memoryStream = new MemoryStream(data);
        var binaryReader = new GFBinaryReader(memoryStream);

        Console.WriteLine($"{binaryReader.ReadString()}");
    }

    private static void HandleServerSocket() {
        var stream = ServerSocket.GetStream();

        var buffer = new byte[65535];

        var readingPacket = false;
        var packetRead = 0;
        var packetSize = 0;
        byte[] packetBuffer = null;

        while (true) {
            int bytesRead;

            try {
                bytesRead = stream.Read(buffer, 0, buffer.Length);
            } catch (Exception) {
                break;
            }

            if (bytesRead > 0) {
                var memoryStream = new MemoryStream(buffer, 0, bytesRead);
                var binaryReader = new BinaryReader(memoryStream);

                while (memoryStream.Position != memoryStream.Length) {
                    if (!readingPacket) {
                        readingPacket = true;
                        packetRead = 0;
                        packetSize = binaryReader.ReadUInt16();
                        packetBuffer = new byte[packetSize];
                    }

                    packetBuffer[packetRead++] = binaryReader.ReadByte();

                    if (packetRead == packetSize) {
                        readingPacket = false;

                        HandleServerRawPacket(packetBuffer);
                    }
                }

            } else break;
        }

        Console.WriteLine("Server disconnected.");
        ServerSocket.Dispose();
        Environment.Exit(0);
    }
}
