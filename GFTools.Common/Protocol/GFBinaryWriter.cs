namespace GFProxy.Protocol;

public class GFBinaryWriter : BinaryWriter {
    public GFBinaryWriter(Stream output) : base(output) { }

    public void WriteByte(byte value) => Write(value);
    public void WriteUInt16(ushort value) => Write(value);
    public void WriteInt16(short value) => Write(value);
    public void WriteInt32(int value) => Write(value);
    public void WriteUInt32(uint value) => Write(value);
    public void WriteSingle(float value) => Write(value);

    public void WriteString(string value) {
        value ??= "";
        WriteUInt16((ushort)value.Length);
        Write(value.ToCharArray());
    }

    public void WriteBytes(byte[] source) {
        for (var i = 0; i < source.Length; i++) {
            WriteByte(source[i]);
        }
    }

    public void WriteBytes(byte[] source, int count) {
        for (var i = 0; i < count; i++) {
            WriteByte(source[i]);
        }
    }

    public void WriteArray<T>(T[] array) where T : CommandBase {
        var arrayLength = (short)array.Length;

        WriteInt16(arrayLength);

        for (var i = 0; i < arrayLength; i++) {
            array[i].Serialize(this);
        }
    }
}
