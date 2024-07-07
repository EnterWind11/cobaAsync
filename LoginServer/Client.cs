using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer
{
    public class Client : IDisposable
    {
        private const int port = 7029;
        private const int bufferSize = 1024;
        private IPEndPoint ipEndPoint;
        private Socket client;
        private NetworkStream stream;

        public Client()
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public async Task RunClientAsync()
        {
            try 
            {
                await client.ConnectAsync(ipEndPoint);
                Console.WriteLine($"Connected to server...");
                
                stream = new NetworkStream(client);
                
                byte[] data = Encoding.ASCII.GetBytes();
                await SendAsync(data);
                
                string receivedData = await ReceiveAsync();
                Console.WriteLine($"Received data from server: {receivedData}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public async Task<string> ReceiveAsync()
        {
            var buffer = new byte[bufferSize];
            int received = await stream.ReadAsync(buffer, 0, buffer.Length);

            Console.WriteLine($"Received {received} bytes from server");

            using var ms = new MemoryStream(buffer, 0, received);
            StreamReader reader = new StreamReader(ms);
            string receivedData = await reader.ReadToEndAsync();

            return receivedData;
        }

        public async Task SendAsync(byte[] data)
        {
            if (stream != null)
            {
                await stream.WriteAsync(data, 0, data.Length);
                Console.WriteLine($"Sent {data.Length} bytes to server");
            }
            else
            {
                Console.WriteLine("No connection to server");
            }
        }

        public void Dispose()
        {
            client?.Close();
            Console.WriteLine("Client connection closed.");
        }
    }
}
