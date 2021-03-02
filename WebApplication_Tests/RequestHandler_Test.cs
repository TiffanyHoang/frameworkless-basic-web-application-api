using System;
using Xunit;
using WebApplication;
namespace WebApplication_Tests
{
    public class RequestHandler_Test
    {
        private readonly Repository _repository;
        private readonly RequestHandler _requestHandler;
        public RequestHandler_Test()
        {
            _repository = new Repository();
            _requestHandler = new RequestHandler(_repository);
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

        // [Fact]
        // public void GetPeopleList_ReturnCorrectListofPeopleInJsonFormat()
        // {

        // }
    }
}
