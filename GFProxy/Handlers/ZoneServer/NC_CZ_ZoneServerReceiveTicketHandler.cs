using GFProxy.Commands.ZoneServer;
using GFProxy.Protocol;
using System.Text.Json;

namespace GFProxy.Handlers.ZoneServer;

public class NC_CZ_ZoneServerReceiveTicketHandler : CommandHandlerBase<ZoneServerClient, NC_CZ_ZoneServerReceiveTicket> {
    public override bool Handle(ZoneServerClient client, NC_CZ_ZoneServerReceiveTicket command) {
        Logger.InfoLine($"{JsonSerializer.Serialize(command)}");
        command.ServerIP = Program.ZoneServerIP;
        command.Port = Program.ZoneServerPort;

        return true;
    }
}
