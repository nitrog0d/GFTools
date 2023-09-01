using GFProxy.Commands.ZoneServer;
using System.Text.Json;

namespace GFProxy.Handlers.ZoneServer;

public class CZ_ZoneServerReceiveTicketHandler : CommandHandlerBase<ZoneServerClient, CZ_ZoneServerReceiveTicket> {
    public override bool Handle(ZoneServerClient client, CZ_ZoneServerReceiveTicket command) {
        Logger.InfoLine($"{JsonSerializer.Serialize(command)}");
        command.ServerIP = Program.ZoneServerIP;
        command.Port = Program.ZoneServerPort;

        return true;
    }
}
