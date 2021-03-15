using System.Collections.Generic;
namespace WebApplication
{
    public class Repository
    {
        private List<Person> _peopleList;

        public string defaultPersonName = "Tiffany";

        public Repository()
        {
            _peopleList = new List<Person>();
            _peopleList.Add(new Person (defaultPersonName));
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

        public void UpdatePerson(Person person, Person updatedPerson)
        {
            var existingPerson = _peopleList.Find(p => p.Name == person.Name);
            existingPerson.Name = updatedPerson.Name;
        }
    }
}