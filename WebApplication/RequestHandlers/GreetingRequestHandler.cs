using System.Net;
using WebApplication.Http;
using WebApplication.Repositories;
using WebApplication.Services;

namespace WebApplication.RequestHandlers
{
    public class GreetingRequestHandler : IGreetingRequestHandler
    {
        private IResponse _response;
        private readonly GreetingService _greetingService;

        public GreetingRequestHandler(GreetingService greetingService)
        {
            _greetingService = greetingService;
        }
        public void HandleRequest(IContext context)
        {
            var _request = context.Request;
            _response = context.Response;

            switch (_request.HttpMethod)
            {
                case "GET":
                    Greeting();
                    break;
                default:
                    HandleInvalidVerbRequest();
                    break;
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
