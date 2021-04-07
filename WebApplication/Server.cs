using System;
using System.Net;
using System.Threading.Tasks;
using WebApplication.Http;
using WebApplication.Repositories;
using WebApplication.RequestHandlers;
using WebApplication.Services;

namespace WebApplication
{
    public class Server
    {
        private readonly HttpListener _listener;
        private readonly RouteController _routeController;

        

        public Server()
        {
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://*:{port}/");

            Repository repository = new Repository();

            GreetingService greetingService = new GreetingService(repository);

            IGreetingRequestHandler greetingRequestHandler = new GreetingRequestHandler(greetingService);
            
            PeopleService peopleService = new PeopleService(repository);

            IPeopleRequestHandler peopleRequestHandler = new PeopleRequestHandler(peopleService);

            _routeController = new RouteController(greetingRequestHandler, peopleRequestHandler);

            _listener.Start();
            Console.WriteLine($"Server started, listening on port {port}");
        }

        public void Close()
        {
            _listener.Close();
        }

        public void ProcessRequest()
        {
            var listenerContext = _listener.GetContext();
            Task.Run(() =>
            {
                var context = new Context(listenerContext);
                _routeController.RequestRouter(context);
                context.Close();
            });
        }
    }
}
