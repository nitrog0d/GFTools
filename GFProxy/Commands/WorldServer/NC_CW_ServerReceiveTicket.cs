using GFProxy.Protocol;
using GFProxy.Protocol.CommandIDs;
using System.Net;

namespace GFProxy.Commands.WorldServer;

public class NC_CW_ServerReceiveTicket : CommandBase {
    public new const ushort CommandID = (ushort)WorldServerCommands.CW_Commands.NC_CW_ServerReceiveTicket;

    public int Unk1 { get; set; }
    public string OwnIP { get; set; }
    public string ServerIP { get; set; }
    public ushort Port { get; set; }
    public byte[] Ticket { get; set; }

    public override void Serialize(GFBinaryWriter writer) {
        writer.WriteInt32(Unk1);
        writer.WriteBytes(IPAddress.Parse(OwnIP).GetAddressBytes());
        writer.WriteBytes(IPAddress.Parse(ServerIP).GetAddressBytes());
        writer.WriteUInt16(Port);
        writer.WriteBytes(Ticket, 8);
    }

    public override void Deserialize(GFBinaryReader reader) {
        Unk1 = reader.ReadInt32();
        OwnIP = new IPAddress(reader.ReadBytes(4)).ToString();
        ServerIP = new IPAddress(reader.ReadBytes(4)).ToString();
        Port = reader.ReadUInt16();
        Ticket = reader.ReadBytes(8);
    }
}
