namespace GFProxy;

public class LoginServer : ProxyServerBase<LoginServerClient> {
    public LoginServer(int proxyPort, string serverIp, int serverPort) : base(proxyPort, serverIp, serverPort) { }
}
