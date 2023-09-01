using GFTools.Common.Protocol;
using GFTools.Common.Protocol.CommandIDs;

namespace GFProxy.Commands.LoginServer;

public class CL_ServerLoginAccount : CommandBase {
    public new const ushort CommandID = (ushort)LoginServerCommands.CL_Commands.CL_ServerLoginAccount;

    public string Account { get; set; }
    public string Password { get; set; }
    public string ClientVersion { get; set; }

    public override void Serialize(GFBinaryWriter writer) {
        writer.WriteString(Account);
        writer.WriteString(Password);
        writer.WriteString(ClientVersion);
    }

    public override void Deserialize(GFBinaryReader reader) {
        Account = reader.ReadString();
        Password = reader.ReadString();
        ClientVersion = reader.ReadString();
    }
}
