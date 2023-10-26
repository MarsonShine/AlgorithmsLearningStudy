using System.Runtime.InteropServices;

namespace HashTables
{
    [StructLayout(LayoutKind.Explicit)]
    struct BytetoUnit32Converter
    {
        [FieldOffset(0)]
        public byte[] Bytes;
        [FieldOffset(0)]
        public uint[] Uints;
    }
}