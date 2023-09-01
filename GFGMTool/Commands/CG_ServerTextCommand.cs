using GFTools.Common.Protocol;
using GFTools.Common.Protocol.CommandIDs;

namespace GFGMTool.Commands;

public class CG_ServerTextCommand : CommandBase {
    public new const ushort CommandID = (ushort)ZoneServerCommands.CG_Commands.CG_ServerTextCommand;

    public string Command { get; set; }

    public override void Serialize(GFBinaryWriter writer) {
        writer.WriteString(Command);
    }

    public override void Deserialize(GFBinaryReader reader) {
        Command = reader.ReadString();
    }
}
