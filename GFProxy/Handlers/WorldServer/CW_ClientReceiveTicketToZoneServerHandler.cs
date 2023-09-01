using GFProxy.Commands.WorldServer;
using System.Text.Json;

namespace GFProxy.Handlers.WorldServer;

public class CW_ClientReceiveTicketToZoneServerHandler : CommandHandlerBase<WorldServerClient, CW_ClientReceiveTicketToZoneServer> {
    public override bool Handle(WorldServerClient client, CW_ClientReceiveTicketToZoneServer command) {
        Logger.InfoLine($"{JsonSerializer.Serialize(command)}");
        command.ServerIP = "127.0.0.1";
        command.Port = Program.ProxyZoneServerPort;

        return true;
    }
}
