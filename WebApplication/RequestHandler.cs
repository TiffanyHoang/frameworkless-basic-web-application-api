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
    }
}