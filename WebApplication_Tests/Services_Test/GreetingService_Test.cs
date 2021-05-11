using Xunit;
using WebApplication;
using System.Collections.Generic;
using WebApplication.Services;
using WebApplication.Repositories;
using System;

namespace WebApplication_Tests
{
    public class GreetingService_Test
    {
        private Repository _repository;
        private GreetingService _greetingService;

        public GreetingService_Test()
        {
            _repository = new Repository();
            _greetingService = new GreetingService(_repository);
        }

        [Fact]
        public void Greeting_ReturnGreetingMessageString()
        {
            var actual = _greetingService.Greeting();
            var time = DateTime.Now.ToString("HH:mm");
            var date = DateTime.Now.ToString("dd MMM yyyy");
            var expected = $"Hello Tiffany - The time on the server is {time} on {date}";
            Assert.Equal(expected, actual);
        }
    }
}
