using GFTools.Common.Protocol;
using GFTools.Common.Protocol.CommandIDs;

namespace GFProxy.Commands.ZoneServer;

public class NC_CZ_ClientHello : CommandBase {
    public new const ushort CommandID = (ushort)ZoneServerCommands.CZ_Commands.NC_CZ_ClientHello;

    public float VersionNumber { get; set; }

    public override void Serialize(GFBinaryWriter writer) {
        writer.WriteSingle(VersionNumber);
    }

    public override void Deserialize(GFBinaryReader reader) {
        VersionNumber = reader.ReadSingle();
    }
}
