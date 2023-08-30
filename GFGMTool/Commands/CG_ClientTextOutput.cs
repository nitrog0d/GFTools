using GFTools.Common.Protocol;
using GFTools.Common.Protocol.CommandIDs;

namespace GFGMTool.Commands;

public class CG_ClientTextOutput : CommandBase {
    public new const ushort CommandID = (ushort)ZoneServerCommands.CG_Commands.NC_CG_ClientTextOutput;

    public byte Type { get; set; }
    public int MsgID { get; set; }
    public string Msg { get; set; }

    public override void Serialize(GFBinaryWriter writer) {
        writer.WriteByte(Type);
        writer.WriteInt32(MsgID);
        writer.WriteString(Msg);
    }

    public override void Deserialize(GFBinaryReader reader) {
        Type = reader.ReadByte();
        MsgID = reader.ReadInt32();
        Msg = reader.ReadString();
    }
}
