using System;
using Xunit;
using System.Net;
using WebApplication;
using System.Net.Http;
using System.IO;
using Moq;
using System.Text;
using System.Collections.Generic;

namespace WebApplication_Tests
{
    public class RequestHandler_Test
    {
        private readonly Repository _repository;
        private readonly RequestHandler _requestHandler;

        private HttpClient _client;
        public RequestHandler_Test()
        {
            _repository = new Repository();
            _requestHandler = new RequestHandler(_repository);
            _client = new HttpClient();
        }


        [Fact]
        public void HandleGreeting_ReturnCorrectGreetingMessage()
        {
            var request = Mock.Of<IRequest>();
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);
            _requestHandler.HandleGreeting(context);
            var time = DateTime.Now.ToString("HH:mm");
            var date = DateTime.Now.ToString("dd MMM yyyy");
            var expectedResponse = $"Hello Tiffany - the time on the server is {time} on {date}";

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);

            response.OutputStream.Position = 0;
            var actualResponse = new StreamReader(response.OutputStream, Encoding.UTF8).ReadToEnd();
            Assert.Equal(expectedResponse, actualResponse);
        }

        [Fact]
        public void HandleGetPeople_RespondListOfPeopleInJsonFormatAndCorrectStatusCode()
        {
            var request = Mock.Of<IRequest>();
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);
            _requestHandler.HandleGetPeople(context);

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);

            response.OutputStream.Position = 0;
            var actualResponse = new StreamReader(response.OutputStream, Encoding.UTF8).ReadToEnd();

            Assert.Equal("[\n  {\n    \"Name\": \"Tiffany\"\n  }\n]", actualResponse);
        }

        [Fact]
        public void HandleCreatePerson_NewPerson_AddPersonToRepositoryAndRespondOKStatusAndNewPersonInJsonFormat()
        {
            var request = Mock.Of<IRequest>(r =>
                r.InputStream == new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\": \"DS\"}")) && r.ContentEncoding == Encoding.UTF8
                );
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);
            _requestHandler.HandleCreatePerson(context);

            var expectedRepo = new List<Person> { new Person("Tiffany"), new Person("DS") };

            Assert.Equal(expectedRepo, _repository.GetPeopleList());

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);

            response.OutputStream.Position = 0;
            var actualResponse = new StreamReader(response.OutputStream, Encoding.UTF8).ReadToEnd();

            Assert.Equal("{\n  \"Name\": \"DS\"\n}", actualResponse);
        }

        [Fact]
        public void HandleCreatePerson_WhenNewPersonSameAsExistingPerson_RespondConflictStatus()
        {
            var request = Mock.Of<IRequest>(r =>
                r.InputStream == new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\": \"Tiffany\"}")) && r.ContentEncoding == Encoding.UTF8
                );
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);
            _requestHandler.HandleCreatePerson(context);

            Assert.Equal((int)HttpStatusCode.Conflict, response.StatusCode);
        }

        [Fact]
        public void HandleUpdatePerson_UpdatePersonInRepositoryAndRespondOKStatusAndUpdatedPersonInJsonFormat()
        {
            _repository.AddPerson(new Person("DS"));
            var request = Mock.Of<IRequest>(r =>
                r.InputStream == new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\": \"DSTeoh\"}")) && r.ContentEncoding == Encoding.UTF8 && r.Url == new Uri("/people/DS")
                );
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);
            _requestHandler.HandleUpdatePerson(context);

            var expectedRepo = new List<Person> { new Person("Tiffany"), new Person("DSTeoh") };

            Assert.Equal(expectedRepo, _repository.GetPeopleList());

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);

            response.OutputStream.Position = 0;
            var actualResponse = new StreamReader(response.OutputStream, Encoding.UTF8).ReadToEnd();

            Assert.Equal("{\n  \"Name\": \"DSTeoh\"\n}", actualResponse);
        }

        [Fact]
        public void HandleUpdatePerson_UpdatedNameSameAsOldName_RespondConflictStatus()
        {
            var request = Mock.Of<IRequest>(r =>
                r.InputStream == new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\": \"Tiffany\"}")) && r.ContentEncoding == Encoding.UTF8 && r.Url == new Uri("/people/Tiffany")
                );
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);
            _requestHandler.HandleUpdatePerson(context);

            Assert.Equal((int)HttpStatusCode.Conflict, response.StatusCode);
        }

        [Fact]
        public void HandleUpdatePerson_UpdatedNameSameAsDefaultPersonName_RespondForbiddenStatus()
        {
            _repository.AddPerson(new Person("Tiff"));
            var request = Mock.Of<IRequest>(r =>
                r.InputStream == new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\": \"Tiffany\"}")) && r.ContentEncoding == Encoding.UTF8 && r.Url == new Uri("/people/Tiff")
                );
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);
            _requestHandler.HandleUpdatePerson(context);

            Assert.Equal((int)HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public void HandleUpdatePerson_OldNameIsNotExisted_RespondNotFoundStatus()
        {
            var request = Mock.Of<IRequest>(r =>
                r.InputStream == new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\": \"DSTeoh\"}")) && r.ContentEncoding == Encoding.UTF8 && r.Url == new Uri("/people/DS")
                );
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);
            _requestHandler.HandleUpdatePerson(context);

            Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public void HandleDeletePerson_DeletePersonInRepositoryAndRespondOKStatus()
        {
            _repository.AddPerson(new Person("DS"));
            _repository.AddPerson(new Person("Mattias"));
            var request = Mock.Of<IRequest>(r => r.Url == new Uri("/people/Mattias"));
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);
            _requestHandler.HandleDeletePerson(context);

            var expectedRepo = new List<Person> { new Person("Tiffany"), new Person("DS") };

            Assert.Equal(expectedRepo, _repository.GetPeopleList());

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void HandleDeletePerson_PersonIsNotExisted_RespondNotFoundStatus()
        {
            var request = Mock.Of<IRequest>(r => r.Url == new Uri("/people/Mattias"));
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);
            _requestHandler.HandleDeletePerson(context);

            Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public void HandleDeletePerson_PersonSameAsDefaultPerson_RespondForbiddenStatus()
        {
            var request = Mock.Of<IRequest>(r => r.Url == new Uri("/people/Tiffany"));
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);
            _requestHandler.HandleDeletePerson(context);

            Assert.Equal((int)HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
