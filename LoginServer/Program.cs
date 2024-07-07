using System.Threading.Tasks;

namespace LoginServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var server = new Server();
            var client = new Client();

            // Start server and client concurrently
            var serverTask = server.RunServerAsync();
            var clientTask = client.RunClientAsync();

            await Task.WhenAll(serverTask, clientTask);
        }
    }
}