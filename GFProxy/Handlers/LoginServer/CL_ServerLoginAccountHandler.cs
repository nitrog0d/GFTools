using GFProxy.Commands.LoginServer;
using System.Text.Json;

namespace GFProxy.Handlers.LoginServer;

public class CL_ServerLoginAccountHandler : CommandHandlerBase<LoginServerClient, CL_ServerLoginAccount> {
    public override bool Handle(LoginServerClient client, CL_ServerLoginAccount command) {
        Logger.InfoLine($"{JsonSerializer.Serialize(command)}");

        command.ClientVersion = "006.761.80.80";

        return true;
    }
}
