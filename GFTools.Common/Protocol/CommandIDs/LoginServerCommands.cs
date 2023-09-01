namespace GFTools.Common.Protocol.CommandIDs;

public static class LoginServerCommands {
    // Client <-> LoginServer
    public enum CL_Commands : ushort {
        CL_ClientGetAccountName = 7,
        CL_ClientGetWorldServers = 5,
        CL_ClientHello = 3,
        CL_ClientLoginFail = 4,
        CL_ClientReceiveTicketToWorldServer = 6,
        CL_ServerHello = 0,
        CL_ServerLoginAccount = 1,
        CL_ServerLoginWorld = 2
    }

    // GatewayServer
    public const ushort G_ClientAccountLogin = 6;
    public const ushort G_ClientAccountLogout = 7;
    public const ushort G_ClientBillingLogin = 11;
    public const ushort G_ClientGetPointInfo = 12;
    public const ushort G_ClientKickUser = 8;
    public const ushort G_ClientReqBuyItem = 13;
    public const ushort G_ClientServerTypeReport = 9;
    public const ushort G_ClientSetWorldStates = 10;
    public const ushort G_ClientTextCommand = 15;
    public const ushort G_ServerAccountLogin = 0;
    public const ushort G_ServerAccountLogout = 1;
    public const ushort G_ServerBillingLogin = 2;
    public const ushort G_ServerCharacterLogin = 16;
    public const ushort G_ServerCharacterLogout = 17;
    public const ushort G_ServerClientTypeReport = 3;
    public const ushort G_ServerGetPointInfo = 4;
    public const ushort G_ServerReqBuyItem = 5;
    public const ushort G_ServerTextCommand = 14;

    // TicketServer
    public const ushort T_ClientGetTicket = 2;
    public const ushort T_ClientUseTicket = 3;
    public const ushort T_ServerGetTicket = 0;
    public const ushort T_ServerUseTicket = 1;
}
