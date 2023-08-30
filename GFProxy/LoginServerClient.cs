using GFTools.Common.Protocol;
using GFTools.Common.Protocol.CommandIDs;

namespace GFProxy;

public class LoginServerClient : ProxyClientBase {

    public override bool HandleClientPacket(ushort commandId, ref byte[] command) {
        Logger.InfoLine($"Client: {Enum.GetName((LoginServerCommands.CL_Commands)commandId)}");

        if (Context.Handlers.TryGetValue(commandId, out var handler)) {
            var memoryStream = new MemoryStream(command);
            var binaryReader = new GFBinaryReader(memoryStream);

            var instance = (CommandBase)Activator.CreateInstance(handler.Item2);
            instance.Deserialize(binaryReader);

            if (handler.Item1.Handle(this, instance)) {
                memoryStream = new MemoryStream();
                var binaryWriter = new GFBinaryWriter(memoryStream);

                instance.Serialize(binaryWriter);

                command = memoryStream.ToArray();
            } else {
                return false;
            }
        }

        return true;
    }

    public override bool HandleServerPacket(ushort commandId, ref byte[] command) {
        Logger.InfoLine($"Server: {Enum.GetName((LoginServerCommands.CL_Commands)commandId)}");

        if (Context.Handlers.TryGetValue(commandId, out var handler)) {
            var memoryStream = new MemoryStream(command);
            var binaryReader = new GFBinaryReader(memoryStream);

            var instance = (CommandBase)Activator.CreateInstance(handler.Item2);
            instance.Deserialize(binaryReader);

            if (handler.Item1.Handle(this, instance)) {
                memoryStream = new MemoryStream();
                var binaryWriter = new GFBinaryWriter(memoryStream);

                instance.Serialize(binaryWriter);

                command = memoryStream.ToArray();
            } else {
                return false;
            }
        }

        return true;
    }
}
