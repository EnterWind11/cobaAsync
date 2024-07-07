using System.IO;
using System.Text;

namespace LoginServer
{
    public class PacketReader : IDisposable
    {
        private BinaryReader reader;
        public const int CheckSumValidation = 6716;

        public PacketReader(MemoryStream stream)
        {
            reader = new BinaryReader(stream);
        }

        public int ReadType()
        {
            return reader.ReadInt16();
        }

        public byte ReadByte()
        {
            return reader.ReadByte();
        }

        public byte[] ReadBytes(int count)
        {
            return reader.ReadBytes(count);
        }

        public short ReadShort()
        {
            return reader.ReadInt16();
        }

        public ushort ReadUShort()
        {
            return reader.ReadUInt16();
        }

        public int ReadInt()
        {
            return reader.ReadInt32();
        }

        public uint ReadUInt()
        {
            return reader.ReadUInt32();
        }

        public long ReadInt64()
        {
            return reader.ReadInt64();
        }

        public ulong ReadUInt64()
        {
            return reader.ReadUInt64();
        }

        public string ReadString()
        {
            int length = reader.ReadByte();
            return Encoding.ASCII.GetString(reader.ReadBytes(length));
        }

        public float ReadFloat()
        {
            return reader.ReadSingle();
        }

        public void Dispose()
        {
            reader.Close();
            reader.Dispose();
        }
    }
}