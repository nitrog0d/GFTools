using GFProxy.Commands.ZoneServer;
using System.Text.Json;

namespace GFProxy.Handlers.ZoneServer;

public class CZ_ClientHelloHandler : CommandHandlerBase<ZoneServerClient, CZ_ClientHello> {
    public override bool Handle(ZoneServerClient client, CZ_ClientHello command) {
        Logger.InfoLine($"{JsonSerializer.Serialize(command)}");

        // command.VersionNumber = 8f;

        return true;
    }
}
