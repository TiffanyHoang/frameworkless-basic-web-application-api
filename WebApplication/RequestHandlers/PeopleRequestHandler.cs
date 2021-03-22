using System.IO;
using System.Net;
using System.Text.Json;
using WebApplication.Repositories;

namespace WebApplication.RequestHandlers
{

    public class PeopleRequestHandler
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
                    HandleGetPeople();
                    break;
                case "POST":
                    HandleCreatePerson();
                    break;
                case "PUT":
                    HandleUpdatePerson();
                    break;
                case "DELETE":
                    HandleDeletePerson();
                    break;
                default:
                    HandleInvalidVerbRequest();
                    break;
            }
        }
        
        private void HandleGetPeople()
        {
            SerialiseJson(_repository.GetPeopleList());
            _response.StatusCode = (int)HttpStatusCode.OK;
        }

        private void HandleCreatePerson()
        {
            using (var reader = new StreamReader(_request.InputStream, _request.ContentEncoding))
            {
                var newPerson = reader.ReadToEnd();
                var newPersonObject = JsonSerializer.Deserialize<Person>(newPerson);
                var isNewPersonExisted = _repository.GetPeopleList().Contains(newPersonObject);

                if (isNewPersonExisted == true)
                {
                    _response.StatusCode = (int)HttpStatusCode.Conflict;
                    return;
                }

                _repository.AddPerson(new Person(newPersonObject.Name));
                SerialiseJson(new Person(newPersonObject.Name));
                _response.StatusCode = (int)HttpStatusCode.OK;
            }
        }
        private void HandleUpdatePerson()
        {
            using (var reader = new StreamReader(_request.InputStream, _request.ContentEncoding))
            {
                var newPerson = reader.ReadToEnd();
                var newPersonObject = JsonSerializer.Deserialize<Person>(newPerson);

                var segments = _request.Url.Segments;
                var oldPersonObject = new Person(segments[2]);

                if (oldPersonObject.Name == newPersonObject.Name)
                {
                    _response.StatusCode = (int)HttpStatusCode.Conflict;
                    return;
                }
                
                if (_repository.defaultPersonName == newPersonObject.Name)
                {
                    _response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return;
                }

                if (!_repository.GetPeopleList().Contains(oldPersonObject))
                {
                    _response.StatusCode = (int)HttpStatusCode.NotFound;
                    return;
                }
               
                _repository.UpdatePerson(oldPersonObject, new Person(newPersonObject.Name));

                SerialiseJson(new Person(newPersonObject.Name));
                _response.StatusCode = (int)HttpStatusCode.OK;
            }
        }

        private void HandleDeletePerson()
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