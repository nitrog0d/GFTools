using GFProxy.Commands.WorldServer;
using GFProxy.Protocol;
using System.Text.Json;

namespace GFProxy.Handlers.WorldServer;

public class NC_CW_ServerReceiveTicketHandler : CommandHandlerBase<WorldServerClient, NC_CW_ServerReceiveTicket> {
    public override bool Handle(WorldServerClient client, NC_CW_ServerReceiveTicket command) {
        Logger.InfoLine($"{JsonSerializer.Serialize(command)}");
        command.ServerIP = Program.WorldServerIP;
        command.Port = Program.WorldServerPort;

        return true;
    }
}
