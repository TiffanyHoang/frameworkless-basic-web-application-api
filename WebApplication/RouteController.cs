using System;
using System.Net;
using System.Text.RegularExpressions;
using WebApplication.Http;
using WebApplication.Repositories;
using WebApplication.RequestHandlers;
namespace WebApplication
{
    public class RouteController
    {
        private readonly IRequestHandler _greetingRequestHandler;
        private readonly IRequestHandler _peopleRequestHandler;
        private readonly IRequestHandler _healthCheckHandler;

        public RouteController(IRequestHandler greetingRequestHandler, IRequestHandler peopleRequestHandler, IRequestHandler healthCheckHandler)
        {
            _greetingRequestHandler = greetingRequestHandler;
            _peopleRequestHandler = peopleRequestHandler;
            _healthCheckHandler = healthCheckHandler;
        }

        public void RequestRouter(IContext context)
        {
            var path = context.Request.Url.AbsolutePath;
            var method = context.Request.HttpMethod;
            Console.WriteLine($"Requesting {method} {path}");
            var rootPath = new Regex("^/$");
            var peoplePath = new Regex("^/people/?");
            var healthPath = new Regex("^/health");

            if (rootPath.IsMatch(path))
            {
                _greetingRequestHandler.HandleRequest(context);
                return;
            }
            
            if (peoplePath.IsMatch(path))
            {
                _peopleRequestHandler.HandleRequest(context);
                return;
            }

            if (healthPath.IsMatch(path))
            {
                _healthCheckHandler.HandleRequest(context);
                return;
            }

            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            Console.WriteLine("Unable to route request. No matching handler found");
        }
    }
}