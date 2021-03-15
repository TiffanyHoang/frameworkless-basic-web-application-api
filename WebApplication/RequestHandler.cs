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

        public string Greeting()
        {
            DateTimeManager dateTimeManager = new DateTimeManager();
            var timeText = $"the time on the server is {dateTimeManager.GetCurrentTime()} on {dateTimeManager.GetCurrentDate()}";

            var peopleListString = "";
            foreach (var person in _repository.GetPeopleList())
            {
                peopleListString += person.Name + ' ';
            }
            return $"Hello {peopleListString}- {timeText}";
        }

        public void CreatePerson(IContext context)
        {
            using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
            {
                var newPerson = reader.ReadToEnd();
                var newPersonJson = JsonSerializer.Deserialize<Person>(newPerson);

                _repository.AddPerson(new Person(newPersonJson.Name));

                context.Response.StatusCode = (int)HttpStatusCode.OK;

                var options = new JsonSerializerOptions { WriteIndented = true };
                var buffer = JsonSerializer.SerializeToUtf8Bytes(new Person(newPersonJson.Name), options);

                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}