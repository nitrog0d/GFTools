namespace GFProxy;

public class WorldServer : ProxyServerBase<WorldServerClient> {
    public WorldServer(int proxyPort, string serverIp, int serverPort) : base(proxyPort, serverIp, serverPort) { }
}
