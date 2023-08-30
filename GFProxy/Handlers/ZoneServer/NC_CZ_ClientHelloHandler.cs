using GFProxy.Commands.ZoneServer;
using System.Text.Json;

namespace GFProxy.Handlers.ZoneServer;

public class NC_CZ_ClientHelloHandler : CommandHandlerBase<ZoneServerClient, NC_CZ_ClientHello> {
    public override bool Handle(ZoneServerClient client, NC_CZ_ClientHello command) {
        Logger.InfoLine($"{JsonSerializer.Serialize(command)}");

        // command.VersionNumber = 8f;

        return true;
    }
}
