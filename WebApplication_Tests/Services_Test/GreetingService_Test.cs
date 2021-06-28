using Xunit;
using WebApplication.Services;
using WebApplication.Repositories;
using System;
using Moq;
using System.Data;

namespace WebApplication_Tests
{
    public class GreetingService_Test
    {
        private GreetingService _greetingService;

        public GreetingService_Test()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("name", typeof(string));
            DataRow dataRow = dataTable.NewRow();
            dataRow["name"] = "Tiffany";
            dataTable.Rows.Add(dataRow);
            var query = "SELECT name from people";
            var database = Mock.Of<IDatabase>(x => x.ExecuteQuery(query) == dataTable);
            var repository = new Repository(database);

            _greetingService = new GreetingService(repository);
        }

        [Fact]
        public void Greeting_ReturnGreetingMessageString()
        {
            var actual = _greetingService.Greeting();
            var time = DateTime.Now.ToUniversalTime().ToString("HH:mm");
            var date = DateTime.Now.ToUniversalTime().ToString("dd MMM yyyy");
            var expected = $"Hello Tiffany - The time on the server is {time} on {date}";
            Assert.Equal(expected, actual);
        }
    }
}
