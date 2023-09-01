using GFProxy.Commands.LoginServer;
using System.Text.Json;

namespace GFProxy.Handlers.LoginServer;

public class CL_ClientHelloHandler : CommandHandlerBase<LoginServerClient, CL_ClientHello> {
    public override bool Handle(LoginServerClient client, CL_ClientHello command) {
        Logger.InfoLine($"{JsonSerializer.Serialize(command)}");

        return true;
    }
}
