namespace GFTools.Common.Protocol;

public abstract class CommandBase
{
    public const ushort CommandID = 0;
    public abstract void Serialize(GFBinaryWriter writer);
    public abstract void Deserialize(GFBinaryReader reader);

    public byte[] Serialize() {
        var memoryStream = new MemoryStream();
        var binaryWriter = new GFBinaryWriter(memoryStream);

        Serialize(binaryWriter);

        return memoryStream.ToArray();
    }

    public void Deserialize(byte[] data) {
        var memoryStream = new MemoryStream(data);
        var binaryReader = new GFBinaryReader(memoryStream);

        Deserialize(binaryReader);
    }

    /*public virtual byte[] Serialize() {
        var memoryStream = new MemoryStream();
        using var binaryWriter = new BinaryWriter(memoryStream);

        SerializeWithReflection(binaryWriter);

        return memoryStream.ToArray();
    }

    public virtual void Deserialize(byte[] data) {
        var memoryStream = new MemoryStream(data);
        using var binaryReader = new BinaryReader(memoryStream);

        DeserializeWithReflection(binaryReader);
    }

    private void DeserializeWithReflection(BinaryReader reader) {
        var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).OrderBy(f => f.MetadataToken).ToArray();

        foreach (var property in properties) {
            property.SetValue(this, DeserializeType(reader, property.PropertyType));
        }
    }

    private object DeserializeType(BinaryReader reader, Type type) {
        if (type.IsAssignableTo(typeof(CommandBase))) {
            var commandBase = (CommandBase)Activator.CreateInstance(type);

            commandBase.DeserializeWithReflection(reader);

            return commandBase;
        }

        if (type.IsArray) {
            var arrayLength = (short)DeserializeType(reader, typeof(short));

            var elementType = type.GetElementType();

            var array = Array.CreateInstance(elementType, arrayLength);

            for (var i = 0; i < arrayLength; i++) {
                array.SetValue(DeserializeType(reader, elementType), i);
            }

            return array;
        }

        switch (type.FullName) {
            case "System.Int16":
                return reader.ReadInt16();
            case "System.UInt16":
                return reader.ReadUInt16();
            case "System.Int32":
                return reader.ReadInt32();
            case "System.UInt32":
                return reader.ReadUInt32();
            case "System.String":
                return reader.ReadGFString();
            default:
                throw new Exception($"Unknown type at deserialization! {type.FullName}");
        }
    }

    private void SerializeWithReflection(BinaryWriter writer) {
        var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).OrderBy(f => f.MetadataToken).ToArray();

        foreach (var property in properties) {
            SerializeType(writer, property.PropertyType, property.GetValue(this));
        }
    }

    private void SerializeType(BinaryWriter writer, Type type, object value) {
        if (type.IsAssignableTo(typeof(CommandBase))) {
            value ??= Activator.CreateInstance(type);

            var commandBase = (CommandBase)value;

            commandBase.SerializeWithReflection(writer);

            return;
        }

        if (type.IsArray) {
            if (value == null) return;

            var array = (object[])value;

            SerializeType(writer, typeof(short), (short)array.Length);

            var arrayType = array.GetType().GetElementType();

            foreach (var entry in array) {
                SerializeType(writer, arrayType, entry);
            }

            return;
        }

        switch (type.FullName) {
            case "System.Int16":
                writer.Write((short)(value ?? (short)0));
                break;
            case "System.UInt16":
                writer.Write((ushort)(value ?? (ushort)0));
                break;
            case "System.Int32":
                writer.Write((int)(value ?? 0));
                break;
            case "System.UInt32":
                writer.Write((uint)(value ?? (uint)0));
                break;
            case "System.String":
                writer.WriteGFString((string)(value ?? ""));
                break;
            default:
                throw new Exception($"Unknown type at serialization! {type.FullName}");
        }
    }*/
}
