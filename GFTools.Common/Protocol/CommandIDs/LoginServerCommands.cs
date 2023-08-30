namespace GFTools.Common.Protocol.CommandIDs;

public static class LoginServerCommands {
    // Client <-> LoginServer
    public enum CL_Commands : ushort {
        NC_CL_ClientGetAccountName = 7,
        NC_CL_ClientGetWorldServers = 5,
        NC_CL_ClientHello = 3,
        NC_CL_ClientLoginFail = 4,
        NC_CL_ClientReceiveTicketToWorldServer = 6,
        NC_CL_ServerHello = 0,
        NC_CL_ServerLoginAccount = 1,
        NC_CL_ServerLoginWorld = 2
    }

    // GatewayServer
    public const ushort NC_G_ClientAccountLogin = 6;
    public const ushort NC_G_ClientAccountLogout = 7;
    public const ushort NC_G_ClientBillingLogin = 11;
    public const ushort NC_G_ClientGetPointInfo = 12;
    public const ushort NC_G_ClientKickUser = 8;
    public const ushort NC_G_ClientReqBuyItem = 13;
    public const ushort NC_G_ClientServerTypeReport = 9;
    public const ushort NC_G_ClientSetWorldStates = 10;
    public const ushort NC_G_ClientTextCommand = 15;
    public const ushort NC_G_ServerAccountLogin = 0;
    public const ushort NC_G_ServerAccountLogout = 1;
    public const ushort NC_G_ServerBillingLogin = 2;
    public const ushort NC_G_ServerCharacterLogin = 16;
    public const ushort NC_G_ServerCharacterLogout = 17;
    public const ushort NC_G_ServerClientTypeReport = 3;
    public const ushort NC_G_ServerGetPointInfo = 4;
    public const ushort NC_G_ServerReqBuyItem = 5;
    public const ushort NC_G_ServerTextCommand = 14;

    // TicketServer
    public const ushort NC_T_ClientGetTicket = 2;
    public const ushort NC_T_ClientUseTicket = 3;
    public const ushort NC_T_ServerGetTicket = 0;
    public const ushort NC_T_ServerUseTicket = 1;
}
