using WebApplication.Repositories;
using Serilog;
using System.Collections.Generic;

namespace WebApplication.Services
{
    public interface IPeopleService
    {
        List<Person> GetPeopleList();

        (int statusCode, Person person) CreatePerson(Person person);

        (int statusCode, Person person) UpdatePerson(Person person, Person oldPerson);

        int DeletePerson(Person person);
    }
}
