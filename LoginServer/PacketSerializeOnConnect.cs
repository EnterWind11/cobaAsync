using System;
using System.IO;

namespace LoginServer
{
    [Serializable]
    public class dmoData
    {
        public short Header { get; set; }
        public ushort PacketData { get; set; }
        public short Data1 { get; set; }
        public short Data2 { get; set; }
        public int Part1 { get; set; }
        public short Part2 { get; set; }
        public short Unknown { get; set; }
        public byte padding { get; set; }
    }

    public class SerializeData
    {
        public MemoryStream Serialize(dmoData data)
        {
            PacketWriter writer = null;
            MemoryStream stream = null;

            try
            {
                writer = new PacketWriter();
                writer.WriteShort(data.Header);
                writer.WriteUShort(data.PacketData);
                writer.WriteShort(data.Data1);
                writer.WriteShort(data.Data2);
                writer.WriteInt(data.Part1);
                writer.WriteShort(data.Part2);
                writer.WriteShort(data.Unknown);
                writer.WriteByte(data.padding);

                stream = writer.Packet;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                writer?.Dispose();
            }

            return stream;
        }
    }
}
