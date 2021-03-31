using System;
using System.Net;
using System.Threading.Tasks;
using WebApplication.Http;
using WebApplication.Repositories;
using WebApplication.RequestHandlers;

namespace WebApplication
{
    public class Server
    {
        private readonly HttpListener _listener;
        private readonly RouteController _routeController;

        public Server()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://*:8080/");

            Repository repository = new Repository();

            IGreetingRequestHandler greetingRequestHandler = new GreetingRequestHandler(repository);

            IPeopleRequestHandler peopleRequestHandler = new PeopleRequestHandler(repository);

            _routeController = new RouteController(repository, greetingRequestHandler, peopleRequestHandler);

            _listener.Start();
            Console.WriteLine("Server started");
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
