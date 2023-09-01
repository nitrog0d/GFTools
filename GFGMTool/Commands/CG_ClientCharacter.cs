using GFTools.Common.Protocol;
using GFTools.Common.Protocol.CommandIDs;

namespace GFGMTool.Commands;

public class CG_ClientCharacter : CommandBase {
    public new const ushort CommandID = (ushort)ZoneServerCommands.CG_Commands.CG_ClientCharacter;

    public int ID { get; set; }

    public override void Serialize(GFBinaryWriter writer) {
        writer.WriteInt32(ID);
    }

    public override void Deserialize(GFBinaryReader reader) {
        ID = reader.ReadInt32();
    }
}
