using GFProxy.Protocol;

namespace GFProxy.Commands.LoginServer;

public class NC_CL_ServerLoginAccount : CommandBase {
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
