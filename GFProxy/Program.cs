using GFTools.Common;

namespace GFProxy;

public static class Program {
    public const int ProxyLoginServerPort = 6969;
    public const string LoginServerIP = "172.18.216.109";
    public const int LoginServerPort = 6543;

    public const int ProxyWorldServerPort = 7070;
    public const string WorldServerIP = "172.18.216.109";
    public const int WorldServerPort = 5567;

    public const int ProxyZoneServerPort = 7071;
    public const string ZoneServerIP = "172.18.216.109";
    public const int ZoneServerPort = 10020;

    public static void Main(string[] args) {
        Logger.InfoLine("Initializing Proxy...");

        _ = new LoginServer(ProxyLoginServerPort, LoginServerIP, LoginServerPort);
        _ = new WorldServer(ProxyWorldServerPort, WorldServerIP, WorldServerPort);
        _ = new ZoneServer(ProxyZoneServerPort, ZoneServerIP, ZoneServerPort);

        Console.Title = "GFProxy";
    }
}
