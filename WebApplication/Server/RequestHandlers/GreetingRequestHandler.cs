using System.IO;
using System.Net;
using System.Text.Json;
using WebApplication.Repositories;

namespace WebApplication.Server.RequestHandlers
{
    public class GreetingRequestHandler
    {
        private readonly Repository _repository;
        private IRequest _request;
        private IResponse _response;
        public GreetingRequestHandler(Repository repository)
        {
            _repository = repository;
        }
        public void HandleRequest(IContext context)
        {
            _request = context.Request;
            _response = context.Response;

            switch (context.Request.HttpMethod)
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
            DateTimeManager dateTimeManager = new DateTimeManager();
            var timeText = $"the time on the server is {dateTimeManager.GetCurrentTime()} on {dateTimeManager.GetCurrentDate()}";

            var peopleListString = "";
            foreach (var person in _repository.GetPeopleList())
            {
                peopleListString += person.Name + ' ';
            }

            var buffer = System.Text.Encoding.UTF8.GetBytes($"Hello {peopleListString}- {timeText}");
            _response.ContentLength64 = buffer.Length;
            _response.OutputStream.Write(buffer, 0, buffer.Length);
            _response.StatusCode = (int)HttpStatusCode.OK;
        }

        private void HandleInvalidVerbRequest() { }
    }
}
