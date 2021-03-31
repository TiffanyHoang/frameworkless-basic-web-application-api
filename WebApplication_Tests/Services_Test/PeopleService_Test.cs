using System;
using Xunit;
using System.Net;
using WebApplication.RequestHandlers;
using WebApplication;
using System.IO;
using Moq;
using System.Text;
using System.Collections.Generic;
using WebApplication.Http;
using WebApplication.Services;
using WebApplication.Repositories;

namespace WebApplication_Tests
{
    public class PeopleService_Test
    {
        private Repository _repository;
        private readonly PeopleRequestHandler _peopleRequestHandler;
        private PeopleService _peopleService;

        public PeopleService_Test()
        {
            _repository = new Repository();
            _peopleService = new PeopleService(_repository);
        }

        [Fact]
        public void GetPeopleList_ReturnListOFPeople()
        {
            var actual = _peopleService.GetPeopleList();
            var expected = new List<Person> { new Person("Tiffany") };
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreatePerson_ShouldAddNewPersonToRepo()
        {
            Person person = new Person("DS");
            _peopleService.CreatePerson(person);
            
            var actual = _peopleService.GetPeopleList();

            var expected = new List<Person> {   new Person("Tiffany"),
                new Person("DS")
            };
            
            Assert.Equal(expected, actual);
        }
    }
}
