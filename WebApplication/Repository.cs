using System.Collections.Generic;
namespace WebApplication
{
    public class Repository
    {
        private List<Person> _peopleList;

        private string _defaultPerson = "Tiffany";

        public Repository()
        {
            _peopleList = new List<Person>();
            _peopleList.Add(new Person (_defaultPerson));
        }

        public List<Person> GetPeopleList()
        {
            return _peopleList;
        }

        public void AddPerson(Person person)
        {
            _peopleList.Add(person);
        }

        public void DeletePerson(Person person)
        {
            _peopleList.Remove(person);
        }
    }
}