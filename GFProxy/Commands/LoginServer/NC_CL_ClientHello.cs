using GFTools.Common.Protocol;

namespace GFProxy.Commands.LoginServer;

public class NC_CL_ClientHello : CommandBase {
    public float Unk1 { get; set; }

    public override void Serialize(GFBinaryWriter writer) {
        writer.WriteSingle(Unk1);
    }

    public override void Deserialize(GFBinaryReader reader) {
        Unk1 = reader.ReadSingle();
    }
}
