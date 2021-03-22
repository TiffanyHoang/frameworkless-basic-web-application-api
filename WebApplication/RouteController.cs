using System;
using System.Net;
using System.Text.RegularExpressions;
using WebApplication.Repositories;
using WebApplication.RequestHandlers;
namespace WebApplication
{
    public class RouteController
    {
        private readonly GreetingRequestHandler _greetingRequestHandler;
        private readonly PeopleRequestHandler _peopleRequestHandler;

        public RouteController(Repository repository)
        {
            _greetingRequestHandler = new GreetingRequestHandler(repository);
            _peopleRequestHandler = new PeopleRequestHandler(repository);
        }

        public void RequestRouter(IContext context)
        {
            var path = context.Request.Url.AbsolutePath;
            var method = context.Request.HttpMethod;
            Console.WriteLine($"Requesting {method} {path}");
            var rootPath = new Regex("^/$");
            var peoplePath = new Regex("^/people/?");

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

            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            Console.WriteLine("Unable to route request. No matching handler found");
        }
    }
}