using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using WebApplication.Repositories;
using Serilog;

namespace WebApplication.Services
{
    public class PeopleService
    {
        private readonly Repository _repository;
        public PeopleService(Repository repository)
        {
            _repository = repository;
        }

        public void AddPerson(Person person)
        {
            _repository.AddPerson(person);
        }

        public List<Person> GetPeopleList()
        {
            try
            {
                return _repository.GetPeopleList();
            }
            catch (Exception e)
            {
                Log.Information(e.ToString());
                throw new HttpRequestException("Error getting people list");
            }
        }

        public (int statusCode, Person person) CreatePerson(Person person)
        {
            try
            {
                var isNewPersonExisted = _repository.GetPeopleList().Contains(person);

                if (isNewPersonExisted == true)
                {
                    return ((int)HttpStatusCode.Conflict, null);
                }

                _repository.AddPerson(new Person(person.Name));
                return ((int)HttpStatusCode.OK, new Person(person.Name));
            }
            catch (Exception e)
            {
                Log.Information(e.ToString());
                throw new HttpRequestException("Error creating new person");
            }
        }

        public (int statusCode, Person person) UpdatePerson(Person person, Person oldPerson)
        {
            try
            {
                if (_repository.defaultPersonName == person.Name)
                {
                    return ((int)HttpStatusCode.Forbidden, null);
                }

                if (!_repository.GetPeopleList().Contains(oldPerson))
                {
                    return ((int)HttpStatusCode.NotFound, null);
                }

                _repository.UpdatePerson(oldPerson, new Person(person.Name));

                return ((int)HttpStatusCode.OK, new Person(person.Name));
            }
            catch (Exception e)
            {
                Log.Information(e.ToString());
                throw new HttpRequestException("Error updating person");
            }
        }

        public int DeletePerson(Person person)
        {
            try
            {
                if (!_repository.GetPeopleList().Contains(person))
                {
                    return (int)HttpStatusCode.NotFound;
                }

                if (person.Name == _repository.defaultPersonName)
                {
                    return (int)HttpStatusCode.Forbidden;
                }

                _repository.DeletePerson(person);
                return (int)HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                Log.Information(e.ToString());
                throw new HttpRequestException("Error deleting person");
            }
        }
    }
}