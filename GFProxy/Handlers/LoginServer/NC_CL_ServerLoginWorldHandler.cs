using GFProxy.Commands.LoginServer;
using GFProxy.Protocol;
using System.Text.Json;

namespace GFProxy.Handlers.LoginServer;

public class NC_CL_ServerLoginWorldHandler : CommandHandlerBase<LoginServerClient, NC_CL_ServerLoginWorld> {
    public override bool Handle(LoginServerClient client, NC_CL_ServerLoginWorld command) {
        // if (command.ServerID > 7000) command.ServerID -= 7000;

        Logger.InfoLine($"{JsonSerializer.Serialize(command)}");

        return true;
    }
}
