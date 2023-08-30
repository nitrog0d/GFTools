using GFTools.Common.Protocol;
using GFTools.Common.Protocol.CommandIDs;

namespace GFProxy.Commands.LoginServer;

public class NC_CL_ClientGetWorldServers : CommandBase {
    public new const ushort CommandID = (ushort)LoginServerCommands.CL_Commands.NC_CL_ClientGetWorldServers;

    public WorldServer[] WorldServers { get; set; }
    public short LastSelectedServer { get; set; }

    public override void Serialize(GFBinaryWriter writer) {
        writer.WriteArray(WorldServers);
        writer.Write(LastSelectedServer);
    }

    public override void Deserialize(GFBinaryReader reader) {
        WorldServers = reader.ReadArray<WorldServer>();
        LastSelectedServer = reader.ReadInt16();
    }

    public class WorldServer : CommandBase {
        public short ID { get; set; }
        public string Name { get; set; }
        public string Unk1 { get; set; }
        public short Unk2 { get; set; }
        public int Unk3 { get; set; }
        public short PlayerCount { get; set; }
        public short Status { get; set; }
        public short GameVersionMatches { get; set; }
        public short CharacterCount { get; set; }
        public short Unk4 { get; set; }

        public override void Serialize(GFBinaryWriter writer) {
            writer.WriteInt16(ID);
            writer.WriteString(Name);
            writer.WriteString(Unk1);
            writer.WriteInt16(Unk2);
            writer.WriteInt32(Unk3);
            writer.WriteInt16(PlayerCount);
            writer.WriteInt16(Status);
            writer.WriteInt16(GameVersionMatches);
            writer.WriteInt16(CharacterCount);
            writer.WriteInt16(Unk4);
        }

        public override void Deserialize(GFBinaryReader reader) {
            ID = reader.ReadInt16();
            Name = reader.ReadString();
            Unk1 = reader.ReadString();
            Unk2 = reader.ReadInt16();
            Unk3 = reader.ReadInt32();
            PlayerCount = reader.ReadInt16();
            Status = reader.ReadInt16();
            GameVersionMatches = reader.ReadInt16();
            CharacterCount = reader.ReadInt16();
            Unk4 = reader.ReadInt16();
        }
    }
}