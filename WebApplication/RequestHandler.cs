using System.IO;
using System.Net;
using System.Text.Json;

namespace WebApplication
{

    public class RequestHandler
    {
        private readonly Repository _repository;
        public RequestHandler(Repository repository)
        {
            _repository = repository;
        }

        public void Greeting(IContext context)
        {
            DateTimeManager dateTimeManager = new DateTimeManager();
            var timeText = $"the time on the server is {dateTimeManager.GetCurrentTime()} on {dateTimeManager.GetCurrentDate()}";

            var peopleListString = "";
            foreach (var person in _repository.GetPeopleList())
            {
                peopleListString += person.Name + ' ';
            }

            var buffer = System.Text.Encoding.UTF8.GetBytes($"Hello {peopleListString}- {timeText}");
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.StatusCode = (int)HttpStatusCode.OK;
        }

        public void GetPeople(IContext context)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var buffer = JsonSerializer.SerializeToUtf8Bytes(_repository.GetPeopleList(), options);
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.StatusCode = (int)HttpStatusCode.OK;
        }

        public void CreatePerson(IContext context)
        {
            using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
            {
                var newPerson = reader.ReadToEnd();
                var newPersonObject = JsonSerializer.Deserialize<Person>(newPerson);
                var isNewPersonExisted = _repository.GetPeopleList().Contains(newPersonObject);

                if (isNewPersonExisted == true)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                }
                else
                {
                    _repository.AddPerson(new Person(newPersonObject.Name));

                    var options = new JsonSerializerOptions { WriteIndented = true };
                    var buffer = JsonSerializer.SerializeToUtf8Bytes(new Person(newPersonObject.Name), options);
                    context.Response.ContentLength64 = buffer.Length;
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                }
            }
        }

        public void HandleUpdatePerson(IContext context)
        {
            using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
            {
                var newPerson = reader.ReadToEnd();
                var newPersonObject = JsonSerializer.Deserialize<Person>(newPerson);

                var segments = context.Request.Url.Segments;
                var oldPersonObject = new Person(segments[2]);

                if (oldPersonObject.Name == newPersonObject.Name)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                }
                else if (_repository.defaultPersonName == newPersonObject.Name)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                }
                else if (!_repository.GetPeopleList().Contains(oldPersonObject))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                }
                else
                {
                    _repository.UpdatePerson(oldPersonObject, new Person(newPersonObject.Name));

                    var options = new JsonSerializerOptions { WriteIndented = true };
                    var buffer = JsonSerializer.SerializeToUtf8Bytes(new Person(newPersonObject.Name), options);
                    context.Response.ContentLength64 = buffer.Length;
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                }

            }
        }

    }
}