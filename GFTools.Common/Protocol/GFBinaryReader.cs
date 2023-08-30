using GFTools.Common.Protocol;

namespace GFTools.Common.Protocol;

public class GFBinaryReader : BinaryReader {
    public GFBinaryReader(Stream input) : base(input) { }

    public override string ReadString() {
        var stringLength = ReadUInt16();
        return new string(ReadChars(stringLength));
    }

    public T[] ReadArray<T>() where T : CommandBase, new() {
        var arraySize = ReadInt16();

        var array = new T[arraySize];

        for (var i = 0; i < arraySize; i++) {
            var newEntry = new T();
            array[i] = newEntry;
            newEntry.Deserialize(this);
        }

        return array;
    }
}
