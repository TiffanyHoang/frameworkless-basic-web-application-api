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
        private readonly string port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
        public Server()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://*:{port}/");

            var database = new Database();
            Repository repository = new Repository(database);

            IGreetingService greetingService = new GreetingService(repository);

            IRequestHandler greetingRequestHandler = new GreetingRequestHandler(greetingService);

            IPeopleService peopleService = new PeopleService(repository);

            IRequestHandler peopleRequestHandler = new PeopleRequestHandler(peopleService);

            IRequestHandler healthCheckHandler = new HealthCheckHandler();

            _routeController = new RouteController(greetingRequestHandler, peopleRequestHandler, healthCheckHandler);
        }

        public async Task Start()
        {
            _listener.Start();
            Console.WriteLine($"Server started, listening on port {port}");
            while (true)
            {
                var listenerContext = await _listener.GetContextAsync();
                var context = new Context(listenerContext);
                _routeController.RequestRouter(context);
                context.Close();
            }
        }
        public void Close()
        {
            _listener.Close();
        }
    }
}
