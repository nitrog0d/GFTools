using GFProxy.Commands.WorldServer;
using GFProxy.Protocol;
using System.Text.Json;

namespace GFProxy.Handlers.WorldServer;

public class NC_CW_ClientReceiveTicketToZoneServerHandler : CommandHandlerBase<WorldServerClient, NC_CW_ClientReceiveTicketToZoneServer> {
    public override bool Handle(WorldServerClient client, NC_CW_ClientReceiveTicketToZoneServer command) {
        Logger.InfoLine($"{JsonSerializer.Serialize(command)}");
        command.ServerIP = "127.0.0.1";
        command.Port = Program.ProxyZoneServerPort;

        return true;
    }
}
