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

        public void CreatePerson(IContext context)
        {
            using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
            {
                var newPerson = reader.ReadToEnd();
                var newPersonObject = JsonSerializer.Deserialize<Person>(newPerson);

                _repository.AddPerson(new Person(newPersonObject.Name));

                context.Response.StatusCode = (int)HttpStatusCode.OK;

                var options = new JsonSerializerOptions { WriteIndented = true };
                var buffer = JsonSerializer.SerializeToUtf8Bytes(new Person(newPersonObject.Name), options);

                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}