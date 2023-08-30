using GFProxy.Commands.LoginServer;
using System.Text.Json;

namespace GFProxy.Handlers.LoginServer;

public class NC_CL_ClientHelloHandler : CommandHandlerBase<LoginServerClient, NC_CL_ClientHello> {
    public override bool Handle(LoginServerClient client, NC_CL_ClientHello command) {
        Logger.InfoLine($"{JsonSerializer.Serialize(command)}");

        return true;
    }
}
