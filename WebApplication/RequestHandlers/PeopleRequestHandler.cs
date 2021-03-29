using System.IO;
using System.Net;
using System.Text.Json;
using WebApplication.Repositories;

namespace WebApplication.RequestHandlers
{

    public class PeopleRequestHandler:IPeopleRequestHandler
    {
        private readonly Repository _repository;
        private IRequest _request;
        private IResponse _response;
        public PeopleRequestHandler(Repository repository)
        {
            _repository = repository;
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
            SerialiseJson(_repository.GetPeopleList());
            _response.StatusCode = (int)HttpStatusCode.OK;
        }

        private void Create()
        {
            using (var reader = new StreamReader(_request.InputStream, _request.ContentEncoding))
            {
                var personJson = reader.ReadToEnd();
                var person = JsonSerializer.Deserialize<Person>(personJson);
                var isNewPersonExisted = _repository.GetPeopleList().Contains(person);

                if (isNewPersonExisted == true)
                {
                    _response.StatusCode = (int)HttpStatusCode.Conflict;
                    return;
                }

                _repository.AddPerson(new Person(person.Name));
                SerialiseJson(new Person(person.Name));
                _response.StatusCode = (int)HttpStatusCode.OK;
            }
        }
        private void Update()
        {
            using (var reader = new StreamReader(_request.InputStream, _request.ContentEncoding))
            {
                var personJson = reader.ReadToEnd();
                var person = JsonSerializer.Deserialize<Person>(personJson);

                var segments = _request.Url.Segments;
                var oldPersonObject = new Person(segments[2]);

                if (oldPersonObject.Name == person.Name)
                {
                    _response.StatusCode = (int)HttpStatusCode.Conflict;
                    return;
                }
                
                if (_repository.defaultPersonName == person.Name)
                {
                    _response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return;
                }

                if (!_repository.GetPeopleList().Contains(oldPersonObject))
                {
                    _response.StatusCode = (int)HttpStatusCode.NotFound;
                    return;
                }
               
                _repository.UpdatePerson(oldPersonObject, new Person(person.Name));

                SerialiseJson(new Person(person.Name));
                _response.StatusCode = (int)HttpStatusCode.OK;
            }
        }

        private void Delete()
        {
            var segments = _request.Url.Segments;
            var deletedPerson = new Person(segments[2]);
            
            if (!_repository.GetPeopleList().Contains(deletedPerson))
            {
                _response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }
            
            if (deletedPerson.Name == _repository.defaultPersonName)
            {
                _response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }
            
            _repository.DeletePerson(deletedPerson);
            _response.StatusCode = (int)HttpStatusCode.OK;
        }

        private void SerialiseJson(object obj)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var buffer = JsonSerializer.SerializeToUtf8Bytes(obj, options);
            _response.ContentLength64 = buffer.Length;
            _response.OutputStream.Write(buffer, 0, buffer.Length);
        }

        private void HandleInvalidVerbRequest() {}
    }
}