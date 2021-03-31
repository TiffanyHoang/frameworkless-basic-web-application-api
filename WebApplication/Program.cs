namespace WebApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            while (true) {
                server.ProcessRequest();
            }
        }
    }
}