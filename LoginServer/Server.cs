using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer
{
    public class Server : IDisposable
    {
        private const int port = 7029;
        private const int bufferSize = 1024;
        private IPEndPoint ipEndPoint;
        private TcpListener listener;

        public Server()
        {
            ipEndPoint = new IPEndPoint(IPAddress.Any, port);
            listener = new TcpListener(ipEndPoint);
        }

        public async Task RunServerAsync()
        {
            try
            {
                listener.Start();
                Console.WriteLine($"Server is listening on port {port}...");

                while (true)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    Console.WriteLine("Accepted a client connection...");
                    _ = HandleClientAsync(client); // Start a new task to handle the client
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                using (client)
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = new byte[bufferSize];
                    int received = await stream.ReadAsync(buffer, 0, buffer.Length);

                    // Menyimpan data yang diterima ke MemoryStream
                    using var ms = new MemoryStream();
                    await ms.WriteAsync(buffer, 0, received);
                    ms.Position = 0; // Mengatur posisi MemoryStream ke awal

                    // Membaca data dari MemoryStream
                    using (StreamReader reader = new StreamReader(ms))
                    {
                        string receivedData = await reader.ReadToEndAsync(); // Menggunakan ReadToEndAsync

                        Console.WriteLine($"Received {received} bytes from client: {receivedData}");

                        // Pastikan PacketID.LOGIN adalah byte array atau string yang diubah menjadi byte array
                        byte[] response = dmoData(PacketID.LOGIN); // Ubah sesuai dengan definisi PacketID.LOGIN
                        await stream.WriteAsync(response, 0, response.Length);
                        Console.WriteLine($"Sent {response.Length} bytes to client");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"A connection error occurred: {ex.Message}");
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine($"Connection was closed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void Dispose()
        {
            listener?.Stop();
            Console.WriteLine("Server stopped.");
        }
    }

    /*public static class PacketID
    {
        public static readonly string LOGIN = "LOGIN_RESPONSE"; // Sesuaikan dengan data yang ingin Anda kirim
    }*/
}
