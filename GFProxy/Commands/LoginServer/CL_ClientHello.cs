using GFTools.Common.Protocol;
using GFTools.Common.Protocol.CommandIDs;

namespace GFProxy.Commands.LoginServer;

public class CL_ClientHello : CommandBase {
    public new const ushort CommandID = (ushort)LoginServerCommands.CL_Commands.CL_ClientHello;

    public float VersionNumber { get; set; }

    public override void Serialize(GFBinaryWriter writer) {
        writer.WriteSingle(VersionNumber);
    }

    public override void Deserialize(GFBinaryReader reader) {
        VersionNumber = reader.ReadSingle();
    }
}
