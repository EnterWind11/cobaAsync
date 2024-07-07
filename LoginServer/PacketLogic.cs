using System.Net.Sockets;
using System.Threading.Tasks;

namespace LoginServer
{
    public class PacketLogic
    {
        public static async Task HandlePacket(Socket clientSocket, PacketID packetId)
        {
            switch (packetId)
            {
                case PacketID.LOGIN:
                    Console.WriteLine("Received login packet");

                    // Create Instance from PacketSerializeOnConnect
                    var dataToSerialize = new dmoData
                    {
                        Header = 0x1100, 
                        PacketData = 0xFFFF
                        Data1 = 0x08C2,
                        Data2 = 0x0200,
                        Part1 = 0x00000000,
                        Part2 = 0x0000,
                        Unknown = 0x012D,
                        padding = 0x1A
                        // Isi sisanya sesuai kebutuhan Anda
                    };

                    var serializer = new SerializeData();
                    var serializedData = serializer.Serialize(dataToSerialize);

                    byte[] data = serializedData.ToArray();
                    await clientSocket.SendAsync(data, SocketFlags.None);
                    break;
                default:
                    Console.WriteLine($"Packet ID {packetId} is not handled");
                    break;
            }
        }
    }
}