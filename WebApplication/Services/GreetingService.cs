using WebApplication.Repositories;
using Serilog;
using System;
using System.Net.Http;

namespace WebApplication.Services
{
    public class GreetingService
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
                DateTimeManager dateTimeManager = new DateTimeManager();
                var timeText = $"The time on the server is {dateTimeManager.GetCurrentTime()} on {dateTimeManager.GetCurrentDate()}";

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