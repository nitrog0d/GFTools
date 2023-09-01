using GFProxy.Commands.LoginServer;
using System.Text.Json;

namespace GFProxy.Handlers.LoginServer;

public class CL_ClientReceiveTicketToWorldServerHandler : CommandHandlerBase<LoginServerClient, CL_ClientReceiveTicketToWorldServer> {
    public override bool Handle(LoginServerClient client, CL_ClientReceiveTicketToWorldServer command) {
        Logger.InfoLine($"{JsonSerializer.Serialize(command)}");
        // command.ServerIP = "127.0.0.1";
        // command.Port = Program.ProxyWorldServerPort;

        return true;
    }
}
