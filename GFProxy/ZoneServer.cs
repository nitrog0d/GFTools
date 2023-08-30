namespace GFProxy;

public class ZoneServer : ProxyServerBase<ZoneServerClient> {
    public ZoneServer(int proxyPort, string serverIp, int serverPort) : base(proxyPort, serverIp, serverPort) { }
}
