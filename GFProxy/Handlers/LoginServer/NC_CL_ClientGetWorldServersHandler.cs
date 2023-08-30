using GFProxy.Commands.LoginServer;
using GFProxy.Protocol;
using System.Text.Json;

namespace GFProxy.Handlers.LoginServer;

public class NC_CL_ClientGetWorldServersHandler : CommandHandlerBase<LoginServerClient, NC_CL_ClientGetWorldServers> {
    public override bool Handle(LoginServerClient client, NC_CL_ClientGetWorldServers command) {
        /*var worldServers = command.WorldServers.ToList();
        var worldServerCount = worldServers.Count;

        for (var i = 0; i < worldServerCount; i++) {
            var worldServer = worldServers[i];
            var newWorldServer = new NC_CL_ClientGetWorldServers.WorldServer() {
                ID = (short)(worldServer.ID + 7000),
                Name = $"Proxy-{worldServer.Name.Split('-')[1]}",
                Unk1 = worldServer.Unk1,
                Unk2 = worldServer.Unk2,
                Unk3 = worldServer.Unk3,
                PlayerCount = worldServer.PlayerCount,
                Status = worldServer.Status,
                GameVersionMatches = worldServer.GameVersionMatches,
                CharacterCount = worldServer.CharacterCount,
                Unk4 = worldServer.Unk4,
            };

            worldServers.Add(newWorldServer);
        }

        command.WorldServers = worldServers.ToArray();*/

        Logger.InfoLine($"{JsonSerializer.Serialize(command)}");

        return true;
    }
}
