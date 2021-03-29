using System;
using System.Net;
using System.Threading.Tasks;
using WebApplication.Repositories;
using WebApplication.RequestHandlers;

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
            listener.Prefixes.Add($"http://*:8080/");
            listener.Start();

            Console.WriteLine("Server started");

            Repository repository = new Repository();
            IGreetingRequestHandler greetingRequestHandler = new GreetingRequestHandler(repository);
            IPeopleRequestHandler peopleRequestHandler = new PeopleRequestHandler(repository);
            RouteController routeController = new RouteController(repository, greetingRequestHandler, peopleRequestHandler);

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