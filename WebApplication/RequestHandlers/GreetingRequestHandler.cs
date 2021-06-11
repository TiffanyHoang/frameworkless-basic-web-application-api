using System.Net;
using WebApplication.Http;
using WebApplication.Repositories;
using WebApplication.Services;

namespace WebApplication.RequestHandlers
{
    public class GreetingRequestHandler : IRequestHandler
    {
        private IResponse _response;
        private readonly GreetingService _greetingService;

        public GreetingRequestHandler(GreetingService greetingService)
        {
            _greetingService = greetingService;
        }
        public void HandleRequest(IContext context)
        {
            var request = context.Request;
            _response = context.Response;
            if (Authentication.ValidateAuthentication(request))
            {
                switch (request.HttpMethod)
                {
                    case "GET":
                        Greeting();
                        break;
                    default:
                        HandleInvalidVerbRequest();
                        break;
                }
            }
            else 
            {
                _response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
        }

        private void Greeting()
        {
            var result = _greetingService.Greeting();
            var buffer = System.Text.Encoding.UTF8.GetBytes(result);
            _response.ContentLength64 = buffer.Length;
            _response.OutputStream.Write(buffer, 0, buffer.Length);
            _response.StatusCode = (int)HttpStatusCode.OK;
        }

        private void HandleInvalidVerbRequest()
        {
            _response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
        }
    }
}
