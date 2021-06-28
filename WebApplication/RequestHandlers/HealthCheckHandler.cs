using System.Net;
using WebApplication.Http;
using WebApplication.Repositories;
using WebApplication.Services;

namespace WebApplication.RequestHandlers
{
    public class HealthCheckHandler : IRequestHandler
    {
        private IResponse _response;
        public void HandleRequest(IContext context)
        {
            var request = context.Request;
            _response = context.Response;
            switch (request.HttpMethod)
            {
                case "GET":
                    HealthCheck();
                    break;
                default:
                    HandleInvalidVerbRequest();
                    break;
            }
        }

        private void HealthCheck()
        {
            _response.StatusCode = (int)HttpStatusCode.OK;
        }

        private void HandleInvalidVerbRequest()
        {
            _response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
        }
    }
}
