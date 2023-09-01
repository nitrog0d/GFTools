using GFProxy.Commands.LoginServer;
using System.Text.Json;

namespace GFProxy.Handlers.LoginServer;

public class CL_ServerLoginWorldHandler : CommandHandlerBase<LoginServerClient, CL_ServerLoginWorld> {
    public override bool Handle(LoginServerClient client, CL_ServerLoginWorld command) {
        // if (command.ServerID > 7000) command.ServerID -= 7000;

        Logger.InfoLine($"{JsonSerializer.Serialize(command)}");

        return true;
    }
}
