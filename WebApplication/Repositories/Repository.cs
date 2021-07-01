using System.Collections.Generic;
using System.Data;

namespace WebApplication.Repositories
{
    public class Repository
    {
        public string defaultPersonName = "Tiffany";
        private readonly IDatabase _database;
        public Repository(IDatabase database)
        {
            _database = database;
        }

        public List<Person> GetPeopleList()
        {
            var query = "SELECT name from people";
            var dataTable = _database.ExecuteQuery(query);
            var peopleData = dataTable.Rows;
            return GetPeopleListFromRowData(peopleData);
        }

        public void AddPerson(Person person)
        {
            var query = $"INSERT INTO people (name) VALUES('{person.Name}');";
            _database.ExecuteQuery(query);
        }
        public void DeletePerson(Person person)
        {
            var query = $"DELETE FROM people WHERE name='{person.Name}';";
            _database.ExecuteQuery(query);
        }
        public void UpdatePerson(Person person, Person updatedPerson)
        {
            var query = $"UPDATE people SET name='{updatedPerson.Name}' WHERE name='{person.Name}';";
            _database.ExecuteQuery(query);
        }

        private static List<Person> GetPeopleListFromRowData(DataRowCollection peopleData)
        {
            var peopleList = new List<Person>();
            foreach (DataRow personData in peopleData)
            {
                peopleList.Add(new Person(personData.Field<string>("name")));
            }
            return peopleList;
        }
    }
}