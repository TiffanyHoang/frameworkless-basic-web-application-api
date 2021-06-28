using System;
using Xunit;
using System.Net;
using WebApplication.RequestHandlers;
using WebApplication;
using System.IO;
using Moq;
using System.Text;
using WebApplication.Http;
using WebApplication.Services;
using System.Collections.Specialized;
using System.Collections.Generic;
namespace WebApplication_Tests
{
    public class PeopleRequestHandler_Test
    {
        private readonly PeopleRequestHandler _peopleRequestHandler;
        private readonly Mock<IPeopleService> _peopleService;
        private readonly NameValueCollection _headers;
        public PeopleRequestHandler_Test()
        {
            var secret = Environment.GetEnvironmentVariable("SECRET");
            _peopleService = new Mock<IPeopleService>();
            _peopleRequestHandler = new PeopleRequestHandler(_peopleService.Object);
            _headers = new NameValueCollection();
            _headers.Add("Authorization", "Basic " + secret);
        }

        [Fact]
        public void HandleGetPeople_RespondListOfPeopleInJsonFormatAndOKStatusCode()
        {
            var request = Mock.Of<IRequest>(r => r.HttpMethod == "GET" &&
                r.Headers == _headers);
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);
            List<Person> peopleList = new List<Person>()
                                    {
                                        new Person("Tiffany")
                                    };
           
            _peopleService.Setup(s => s.GetPeopleList()).Returns(peopleList);

            _peopleRequestHandler.HandleRequest(context);

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);

            response.OutputStream.Position = 0;
            var actualResponse = new StreamReader(response.OutputStream, Encoding.UTF8).ReadToEnd();

            Assert.Equal("[\n  {\n    \"Name\": \"Tiffany\"\n  }\n]", actualResponse);
        }

        [Fact]
        public void HandleCreatePerson_NewPerson_RespondOKStatusAndNewPersonInJsonFormat()
        {
            var request = Mock.Of<IRequest>(r =>
                r.HttpMethod == "POST" &&
                r.InputStream == new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\": \"DS\"}")) &&
                r.ContentEncoding == Encoding.UTF8 &&
                r.Headers == _headers
                );
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            var person = new Person("DS");

            _peopleService.Setup(s => s.CreatePerson(person)).Returns(((int)HttpStatusCode.OK, person));

            _peopleRequestHandler.HandleRequest(context);

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);

            response.OutputStream.Position = 0;
            var actualResponse = new StreamReader(response.OutputStream, Encoding.UTF8).ReadToEnd();

            Assert.Equal("{\n  \"Name\": \"DS\"\n}", actualResponse);
        }

        [Fact]
        public void HandleCreatePerson_WhenNewPersonSameAsExistingPerson_RespondConflictStatus()
        {
            var request = Mock.Of<IRequest>(r =>
                r.HttpMethod == "POST" &&
                r.InputStream == new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\": \"Tiffany\"}")) && r.ContentEncoding == Encoding.UTF8  &&
                r.Headers == _headers
                );
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            var person = new Person("Tiffany");
            _peopleService.Setup(s => s.CreatePerson(person)).Returns(((int)HttpStatusCode.Conflict, null));

            _peopleRequestHandler.HandleRequest(context);

            Assert.Equal((int)HttpStatusCode.Conflict, response.StatusCode);
        }

        [Fact]
        public void HandleUpdatePerson_RespondOKStatusAndUpdatedPersonInJsonFormat()
        {
            var request = Mock.Of<IRequest>(r =>
                r.HttpMethod == "PUT" &&
                r.InputStream == new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\": \"DSTeoh\"}")) &&
                r.ContentEncoding == Encoding.UTF8 && r.Url == new Uri("/people/DS") &&
                r.Headers == _headers
                );
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            var oldPerson = new Person("DS");
            var person = new Person("DSTeoh");

            _peopleService.Setup(s => s.UpdatePerson(person,oldPerson)).Returns(((int)HttpStatusCode.OK, person));

            _peopleRequestHandler.HandleRequest(context);

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);

            response.OutputStream.Position = 0;
            var actualResponse = new StreamReader(response.OutputStream, Encoding.UTF8).ReadToEnd();

            Assert.Equal("{\n  \"Name\": \"DSTeoh\"\n}", actualResponse);
        }

        [Fact]
        public void HandleUpdatePerson_UpdatedNameSameAsOldName_RespondConflictStatus()
        {
            var request = Mock.Of<IRequest>(r =>
                r.HttpMethod == "PUT" &&
                r.InputStream == new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\": \"Tiffany\"}")) &&
                r.ContentEncoding == Encoding.UTF8 &&
                r.Url == new Uri("/people/Tiffany") &&
                r.Headers == _headers
                );
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            _peopleRequestHandler.HandleRequest(context);

            Assert.Equal((int)HttpStatusCode.Conflict, response.StatusCode);
        }

        [Fact]
        public void HandleUpdatePerson_UpdatedNameSameAsDefaultPersonName_RespondForbiddenStatus()
        {
            var request = Mock.Of<IRequest>(r =>
                r.HttpMethod == "PUT" &&
                r.InputStream == new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\": \"Tiffany\"}")) &&
                r.ContentEncoding == Encoding.UTF8 &&
                r.Url == new Uri("/people/Tiff") &&
                r.Headers == _headers
                );
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);
            
            var person = new Person("Tiffany");
            var oldPerson = new Person("Tiff");

            _peopleService.Setup(s => s.UpdatePerson(person,oldPerson)).Returns(((int)HttpStatusCode.Forbidden, null));

            _peopleRequestHandler.HandleRequest(context);

            Assert.Equal((int)HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public void HandleUpdatePerson_OldNameIsNotExisted_RespondNotFoundStatus()
        {
           
            var request = Mock.Of<IRequest>(r =>
                r.HttpMethod == "PUT" &&
                r.InputStream == new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\": \"DSTeoh\"}")) &&
                r.ContentEncoding == Encoding.UTF8 &&
                r.Url == new Uri("/people/DS") &&
                r.Headers == _headers
                );
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            var person = new Person("DSTeoh");
            var oldPerson = new Person("DS");

            _peopleService.Setup(s => s.UpdatePerson(person,oldPerson)).Returns(((int)HttpStatusCode.NotFound, null));

            _peopleRequestHandler.HandleRequest(context);

            Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public void HandleDeletePerson_RespondOKStatus()
        {
            var request = Mock.Of<IRequest>(r =>
                r.HttpMethod == "DELETE" &&
                r.Url == new Uri("/people/Mattias") &&
                r.Headers == _headers
                );
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);
            
            var person = new Person("Mattias");

            _peopleService.Setup(s => s.DeletePerson(person)).Returns(((int)HttpStatusCode.OK));

            _peopleRequestHandler.HandleRequest(context);

            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void HandleDeletePerson_PersonIsNotExisted_RespondNotFoundStatus()
        {
            var request = Mock.Of<IRequest>(r =>
                r.HttpMethod == "DELETE" &&
                r.Url == new Uri("/people/Mattias") &&
                r.Headers == _headers
                );
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            var person = new Person("Mattias");

            _peopleService.Setup(s => s.DeletePerson(person)).Returns((int)HttpStatusCode.NotFound);

            _peopleRequestHandler.HandleRequest(context);

            Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public void HandleDeletePerson_PersonSameAsDefaultPerson_RespondForbiddenStatus()
        {
            var request = Mock.Of<IRequest>(r =>
                r.HttpMethod == "DELETE" &&
                r.Url == new Uri("/people/Tiffany") &&
                r.Headers == _headers
                );
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            var person = new Person("Tiffany");

            _peopleService.Setup(s => s.DeletePerson(person)).Returns((int)HttpStatusCode.Forbidden);

            _peopleRequestHandler.HandleRequest(context);

            Assert.Equal((int)HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public void InvalidMethod_RespondMethodNotAllowedStatus()
        {
            var request = Mock.Of<IRequest>(r =>
                r.HttpMethod == "INVALIDMETHOD" &&
                r.Url == new Uri("/people") &&
                r.Headers == _headers
                );
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            _peopleRequestHandler.HandleRequest(context);
            
            Assert.Equal((int)HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }
        [Fact]
        public void ValidateAuthentication_NoHeader_ReturnUnauthorizedStatus()
        {
            var request = Mock.Of<IRequest>(r => r.HttpMethod == "GET");
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            _peopleRequestHandler.HandleRequest(context);
            
            Assert.Equal((int)HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
