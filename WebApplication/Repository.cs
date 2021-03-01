using System.Collections.Generic;
namespace WebApplication
{
    public class Repository
    {
        private List<Person> _peopleList;
        
        public Repository()
        {
            _peopleList = new List<Person>();
            const string defaultPerson = "Tiffany";
            _peopleList.Add(new Person (defaultPerson));
        }

        public List<Person> GetPeopleList()
        {
            return _peopleList;
        }
    }
}