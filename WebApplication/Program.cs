using System.Threading.Tasks;

namespace WebApplication
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Server server = new Server();
            await server.Start();
        }
    }
}