using GFProxy.Commands.LoginServer;
using System.Text.Json;

namespace GFProxy.Handlers.LoginServer;

public class NC_CL_ServerLoginAccountHandler : CommandHandlerBase<LoginServerClient, NC_CL_ServerLoginAccount> {
    public override bool Handle(LoginServerClient client, NC_CL_ServerLoginAccount command) {
        Logger.InfoLine($"{JsonSerializer.Serialize(command)}");

        command.ClientVersion = "006.761.80.80";

        return true;
    }
}
