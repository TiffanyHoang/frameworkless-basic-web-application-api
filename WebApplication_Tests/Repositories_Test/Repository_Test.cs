using Xunit;
using System.Collections.Generic;
using WebApplication.Repositories;
using WebApplication;

namespace WebApplication_Tests
{
    public class Repository_Test
    {
        private readonly Repository _repository;
        public Repository_Test()
        {
            _repository = new Repository();
        }
        
        [Fact]
        public void GetPeopleList_ReturnCorrectListOfPeople()
        {
            var actual = _repository.GetPeopleList();
            var expected = new List<Person>{
                                new Person("Tiffany")
                            };
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddPersonToPeopleList_ReturnCorrectListOfPeople()
        {
            _repository.AddPerson(new Person("DS"));
            var actual = _repository.GetPeopleList();
            var expected = new List<Person>{
                                new Person("Tiffany"),
                                new Person("DS")
                            };
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DeletePersonFromPeopleList_ReturnCorrectListOfPeople()
        {
            _repository.DeletePerson(new Person("Tiffany"));
            var actual = _repository.GetPeopleList();
            var expected = new List<Person>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UpdatePersonInPeopleList_ReturnCorrectListOfPeople()
        {
            Person person = new Person("DS");
            _repository.AddPerson(person);
            Person updatedPerson = new Person ("Mattias");
            _repository.UpdatePerson(person, updatedPerson);
            var actual = _repository.GetPeopleList();
            var expected = new List<Person>{
                                new Person("Tiffany"),
                                new Person("Mattias"),
                            };
            Assert.Equal(expected, actual);
        }
    }
}