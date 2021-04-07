using Xunit;
using System.Net;
using WebApplication;
using System.Collections.Generic;
using WebApplication.Services;
using WebApplication.Repositories;

namespace WebApplication_Tests
{
    public class PeopleService_Test
    {
        private Repository _repository;
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
        public void CreatePerson_ShouldAddNewPersonToRepoAndReturnOKStatusAndNewPerson()
        {
            Person person = new Person("DS");
            var actualReturnResult = _peopleService.CreatePerson(person);

            var expectedReturnResult = ((int)HttpStatusCode.OK, new Person("DS"));

            var actualPeopleList = _peopleService.GetPeopleList();

            var expectedPeopleList = new List<Person> {
                new Person("Tiffany"),
                new Person("DS")
            };

            Assert.Equal(expectedPeopleList, actualPeopleList);
            Assert.Equal(expectedReturnResult, actualReturnResult);
        }

        [Fact]
        public void CreatePerson_PersonExisted_ReturnConflictStatusAndNull()
        {
            Person person = new Person("Tiffany");
            var actualReturnResult = _peopleService.CreatePerson(person);

            var expectedReturnResult = ((int)HttpStatusCode.Conflict, Null: (Person)null);

            Assert.Equal(expectedReturnResult, actualReturnResult);
        }

        [Fact]
        public void UpdatePerson_ShouldUpdatePersonInRepoAndReturnOKStatusAndNewPerson()
        {
            Person person = new Person("DS");
            _peopleService.CreatePerson(person);

            Person updatePerson = new Person("DSTeoh");
            var actualReturnResult = _peopleService.UpdatePerson(updatePerson, person);

            var expectedReturnResult = ((int)HttpStatusCode.OK, new Person("DSTeoh"));

            var actualPeopleList = _peopleService.GetPeopleList();

            var expectedPeopleList = new List<Person> {
                new Person("Tiffany"),
                new Person("DSTeoh")
            };

            Assert.Equal(expectedPeopleList, actualPeopleList);
            Assert.Equal(expectedReturnResult, actualReturnResult);
        }

        [Fact]
        public void UpdatePerson_UpdatedNameSameAsDefaultPersonName_ReturnForbiddenStatus()
        {
            Person person = new Person("Tiffany");
            Person oldPerson = new Person("DS");

            var actualReturnResult = _peopleService.UpdatePerson(person, oldPerson);

            var expectedReturnResult = ((int)HttpStatusCode.Forbidden, Null: (Person)null);

            Assert.Equal(expectedReturnResult, actualReturnResult);
        }

        [Fact]
        public void UpdatePerson_OldNameIsNotExisted_ReturnNotFoundStatus()
        {
            Person person = new Person("DSTeoh");
            Person oldPerson = new Person("DS");

            var actualReturnResult = _peopleService.UpdatePerson(person, oldPerson);

            var expectedReturnResult = ((int)HttpStatusCode.NotFound, Null: (Person)null);

            Assert.Equal(expectedReturnResult, actualReturnResult);
        }

        [Fact]
        public void DeletePerson_ShouldDeletePersonInRepoAndReturnOKStatus()
        {
            Person person = new Person("DS");
            _peopleService.CreatePerson(person);

            var expectedReturnResult = ((int)HttpStatusCode.OK);

            var actualReturnResult= _peopleService.DeletePerson(person);

            var actualPeopleList = _peopleService.GetPeopleList();

            var expectedPeopleList = new List<Person> {
                new Person("Tiffany")
            };

            Assert.Equal(expectedPeopleList, actualPeopleList);

            Assert.Equal(expectedReturnResult, actualReturnResult);
        }

        [Fact]
        public void DeletePerson_PersonIsNotExisted_ReturnNotFoundStatus()
        {
            Person person = new Person("DS");

            var expectedReturnResult = ((int)HttpStatusCode.NotFound);

            var actualReturnResult= _peopleService.DeletePerson(person);

            Assert.Equal(expectedReturnResult, actualReturnResult);
        }

        [Fact]
        public void DeletePerson_PersonSameAsDefaultPerson_ReturnNotFoundStatus()
        {
            Person person = new Person("Tiffany");

            var expectedReturnResult = ((int)HttpStatusCode.Forbidden);

            var actualReturnResult= _peopleService.DeletePerson(person);

            Assert.Equal(expectedReturnResult, actualReturnResult);
        }
    }
}
