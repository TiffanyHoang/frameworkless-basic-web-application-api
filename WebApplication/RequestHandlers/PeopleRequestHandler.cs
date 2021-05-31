using System.IO;
using System.Net;
using System.Text.Json;
using WebApplication.Http;
using WebApplication.Services;

namespace WebApplication.RequestHandlers
{

    public class PeopleRequestHandler : IPeopleRequestHandler
    {
        private IRequest _request;
        private IResponse _response;
        private readonly PeopleService _peopleService;
        public PeopleRequestHandler(PeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        public void HandleRequest(IContext context)
        {
            _request = context.Request;
            _response = context.Response;

            switch (_request.HttpMethod)
            {
                case "GET":
                    Get();
                    break;
                case "POST":
                    Create();
                    break;
                case "PUT":
                    Update();
                    break;
                case "DELETE":
                    Delete();
                    break;
                default:
                    HandleInvalidVerbRequest();
                    break;
            }
        }

        private void Get()
        {
            var peopleList = _peopleService.GetPeopleList();
            SerialiseJson(peopleList);
            _response.StatusCode = (int)HttpStatusCode.OK;
        }

        private void Create()
        {
            using (var reader = new StreamReader(_request.InputStream, _request.ContentEncoding))
            {
                var personJson = reader.ReadToEnd();
                var person = JsonSerializer.Deserialize<Person>(personJson);
            
                var result = _peopleService.CreatePerson(person);
                
                if (result.statusCode == (int)HttpStatusCode.Conflict)
                {
                    _response.StatusCode = result.statusCode;
                    return;
                }

                SerialiseJson(result.person);
                _response.StatusCode = result.statusCode;
            }

        }
        private void Update()
        {
            using (var reader = new StreamReader(_request.InputStream, _request.ContentEncoding))
            {
                var personJson = reader.ReadToEnd();
                var person = JsonSerializer.Deserialize<Person>(personJson);

                var segments = _request.Url.Segments;
                var oldPerson = new Person(segments[2]);

                if (oldPerson.Name == person.Name)
                {
                    _response.StatusCode = (int)HttpStatusCode.Conflict;
                    return;
                }

                var result = _peopleService.UpdatePerson(person, oldPerson);
                if (result.statusCode == (int)HttpStatusCode.Forbidden)
                {
                    _response.StatusCode = result.statusCode;
                    return;
                }

                if (result.statusCode == (int)HttpStatusCode.NotFound)
                {
                    _response.StatusCode = result.statusCode;
                    return;
                }

                SerialiseJson(result.person);
                _response.StatusCode = result.statusCode;
            }
        }

        private void Delete()
        {
            var segments = _request.Url.Segments;
            var deletedPerson = new Person(segments[2]);

            var statusCode = _peopleService.DeletePerson(deletedPerson);

            if (statusCode == (int)HttpStatusCode.NotFound)
            {
                _response.StatusCode = statusCode;
                return;
            }

            if (statusCode == (int)HttpStatusCode.Forbidden)
            {
                _response.StatusCode = statusCode;
                return;
            }

            _response.StatusCode = statusCode;
        }

        private void SerialiseJson(object obj)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var buffer = JsonSerializer.SerializeToUtf8Bytes(obj, options);
            _response.ContentLength64 = buffer.Length;
            _response.OutputStream.Write(buffer, 0, buffer.Length);
        }

        private void HandleInvalidVerbRequest()
        {
            _response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
        }
    }
}