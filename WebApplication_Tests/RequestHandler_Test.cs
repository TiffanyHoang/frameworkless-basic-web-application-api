using System;
using Xunit;
using System.Net;
using WebApplication;
using System.Net.Http;
using System.IO;
using Moq;
using System.Text;

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
        public void Greeting_ReturnCorrectGreetingMessage()
        {
            var responseString = _requestHandler.Greeting();
            var time = DateTime.Now.ToString("HH:mm");
            var date = DateTime.Now.ToString("dd MMM yyyy");
            var expected = $"Hello Tiffany - the time on the server is {time} on {date}";

            Assert.Equal(expected, responseString);
        }

        [Fact]
        public void CreatePerson_AddPersonToRepositoryAndRespondCorrectly()
        {
            var request = Mock.Of<IRequest>(r =>
                r.InputStream == new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\": \"DS\"}")) && r.ContentEncoding == Encoding.UTF8
                );
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);
            _requestHandler.CreatePerson(context);

            Assert.Equal((int) HttpStatusCode.OK, response.StatusCode);

            response.OutputStream.Position = 0;
            var actualResponse = new StreamReader(response.OutputStream, Encoding.UTF8).ReadToEnd();

            Assert.Equal("{\n  \"Name\": \"DS\"\n}", actualResponse);
        }
    }
}
