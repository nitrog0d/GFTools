using GFTools.Common.Protocol;
using GFTools.Common.Protocol.CommandIDs;

namespace GFGMTool.Commands;

public class CG_ClientCharacterList : CommandBase {
    public new const ushort CommandID = (ushort)ZoneServerCommands.CG_Commands.CG_ClientCharacterList;

    public Character[] Characters { get; set; }

    public override void Serialize(GFBinaryWriter writer) {

    }

    public override void Deserialize(GFBinaryReader reader) {
        var arraySize = reader.ReadUInt32();

        Characters = new Character[arraySize];

        for (var i  = 0; i < arraySize; i++) {
            var character = new Character();
            character.Deserialize(reader);

            Characters[i] = character;
        }
    }

    public class Character : CommandBase {
        public int ID { get; set; }
        public string Name { get; set; }
        public string AccountName { get; set; }
        public short Level { get; set; }
        public float LocateX { get; set; }
        public float LocateY { get; set; }
        public short NodeID { get; set; }

        public override void Serialize(GFBinaryWriter writer) {

        }

        public override void Deserialize(GFBinaryReader reader) {
            ID = reader.ReadInt32();
            Name = reader.ReadString();
            AccountName = reader.ReadString();
            Level = reader.ReadInt16();
            LocateX = reader.ReadSingle();
            LocateY = reader.ReadSingle();
            NodeID = reader.ReadInt16();
        }
    }
}
