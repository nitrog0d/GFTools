using GFTools.Common.Protocol;
using GFTools.Common.Protocol.CommandIDs;

namespace GFGMTool.Commands;

public class CG_ServerLogin : CommandBase {
    public new const ushort CommandID = (ushort)ZoneServerCommands.CG_Commands.CG_ServerLogin;

    public string Account { get; set; }
    public string Password { get; set; }

    public override void Serialize(GFBinaryWriter writer) {
        writer.WriteString(Account);
        writer.WriteString(Password);
    }

    public override void Deserialize(GFBinaryReader reader) {
        Account = reader.ReadString();
        Password = reader.ReadString();
    }
}
