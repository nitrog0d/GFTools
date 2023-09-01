using GFProxy.Commands.WorldServer;
using System.Text.Json;

namespace GFProxy.Handlers.WorldServer;

public class CW_ServerReceiveTicketHandler : CommandHandlerBase<WorldServerClient, CW_ServerReceiveTicket> {
    public override bool Handle(WorldServerClient client, CW_ServerReceiveTicket command) {
        Logger.InfoLine($"{JsonSerializer.Serialize(command)}");
        command.ServerIP = Program.WorldServerIP;
        command.Port = Program.WorldServerPort;

        return true;
    }
}
