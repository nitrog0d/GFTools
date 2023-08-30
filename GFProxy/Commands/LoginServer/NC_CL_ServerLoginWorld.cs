using GFProxy.Protocol;
using GFProxy.Protocol.CommandIDs;

namespace GFProxy.Commands.LoginServer;

public class NC_CL_ServerLoginWorld : CommandBase {
    public new const ushort CommandID = (ushort)LoginServerCommands.CL_Commands.NC_CL_ServerLoginWorld;

    public short ServerID { get; set; }
    public string Account { get; set; }
    public string Password { get; set; }

    public override void Serialize(GFBinaryWriter writer) {
        writer.WriteInt16(ServerID);
        writer.WriteString(Account);
        writer.WriteString(Password);
    }

    public override void Deserialize(GFBinaryReader reader) {
        ServerID = reader.ReadInt16();
        Account = reader.ReadString();
        Password = reader.ReadString();
    }
}
