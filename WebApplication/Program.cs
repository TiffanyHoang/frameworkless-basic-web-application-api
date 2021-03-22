using System;
using System.Net;
using System.Threading.Tasks;
using WebApplication.Repositories;
namespace WebApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }
        static void Run()
        {
            var listener = new HttpListener();
            listener.Prefixes.Add($"http://*:8081/");
            listener.Start();

            Console.WriteLine("Server started");

            Repository repository = new Repository();
            RouteController routeController = new RouteController(repository);

            while (true)
            {
                var listenerContext = listener.GetContext(); 
                var context = new Context(listenerContext);
                Task.Run(() =>
                {
                    routeController.RequestRouter(context);
                });
            }
        }
    }
}