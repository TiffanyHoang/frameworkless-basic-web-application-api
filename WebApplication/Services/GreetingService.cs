using WebApplication.Repositories;
using Serilog;
using System;
using System.Net.Http;

namespace WebApplication.Services
{
    public class GreetingService : IGreetingService
    {
        private readonly Repository _repository;
        public GreetingService(Repository repository)
        {
            _repository = repository;
        }

        public string Greeting()
        {
            try
            {
                var timeText = $"The time on the server is {DateTimeManager.GetCurrentTime()} on {DateTimeManager.GetCurrentDate()}";
                var peopleListString = "";
                foreach (var person in _repository.GetPeopleList())
                {
                    peopleListString += person.Name + ' ';
                }
                return $"Hello {peopleListString}- {timeText}";
            }
            catch (Exception e)
            {
                Log.Information(e.ToString());
                throw new HttpRequestException("Error getting greeting message");
            }

        }
    }
}