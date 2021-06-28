using Xunit;
using System.Net;
using WebApplication;
using System.Collections.Generic;
using WebApplication.Services;
using WebApplication.Repositories;
using Moq;
using System.Data;

namespace WebApplication_Tests
{
    public class PeopleService_Test
    {
        private readonly PeopleService _peopleService;
        private readonly DataTable _dataTable;
        public PeopleService_Test()
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("name", typeof(string));
            DataRow dataRow = _dataTable.NewRow();
            dataRow["name"] = "Tiffany";
            _dataTable.Rows.Add(dataRow);
            var query = "SELECT name from people";
            var database = Mock.Of<IDatabase>(x => x.ExecuteQuery(query) == _dataTable);
            var repository = new Repository(database);

            _peopleService = new PeopleService(repository);
        }

        [Fact]
        public void GetPeopleList_ReturnListOFPeople()
        {
            var actual = _peopleService.GetPeopleList();
            var expected = new List<Person> { new Person("Tiffany") };
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreatePerson_ReturnOKStatusAndNewPerson()
        {
            Person person = new Person("DS");
            var actualReturnResult = _peopleService.CreatePerson(person);

            var expectedReturnResult = ((int)HttpStatusCode.OK, new Person("DS"));

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
        public void UpdatePerson_ReturnOKStatusAndNewPerson()
        {
            DataRow dataRow = _dataTable.NewRow();
            dataRow["name"] = "DS";
            _dataTable.Rows.Add(dataRow);

            Person updatePerson = new Person("DSTeoh");
            var actualReturnResult = _peopleService.UpdatePerson(updatePerson, new Person("DS"));

            var expectedReturnResult = ((int)HttpStatusCode.OK, new Person("DSTeoh"));

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
        public void DeletePerson_ReturnOKStatus()
        {
            DataRow dataRow = _dataTable.NewRow();
            dataRow["name"] = "DS";
            _dataTable.Rows.Add(dataRow);

            var expectedReturnResult = ((int)HttpStatusCode.OK);

            var actualReturnResult = _peopleService.DeletePerson(new Person("DS"));

            Assert.Equal(expectedReturnResult, actualReturnResult);
        }

        [Fact]
        public void DeletePerson_PersonIsNotExisted_ReturnNotFoundStatus()
        {
            Person person = new Person("DS");

            var expectedReturnResult = ((int)HttpStatusCode.NotFound);

            var actualReturnResult = _peopleService.DeletePerson(person);

            Assert.Equal(expectedReturnResult, actualReturnResult);
        }

        [Fact]
        public void DeletePerson_PersonSameAsDefaultPerson_ReturnNotFoundStatus()
        {
            Person person = new Person("Tiffany");

            var expectedReturnResult = ((int)HttpStatusCode.Forbidden);

            var actualReturnResult = _peopleService.DeletePerson(person);

            Assert.Equal(expectedReturnResult, actualReturnResult);
        }
    }
}
