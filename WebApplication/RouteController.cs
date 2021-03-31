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
        private readonly IGreetingRequestHandler _greetingRequestHandler;
        private readonly IPeopleRequestHandler _peopleRequestHandler;

        public RouteController(Repository repository, IGreetingRequestHandler greetingRequestHandler, IPeopleRequestHandler peopleRequestHandler)
        {
            _greetingRequestHandler = greetingRequestHandler;
            _peopleRequestHandler = peopleRequestHandler;
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