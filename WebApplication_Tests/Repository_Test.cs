using Xunit;
using System.Collections.Generic;
using WebApplication;
namespace WebApplication_Tests
{
    public class Repository_Test
    {
        [Fact]
        public void GetPeopleList_ReturnCorrectListOfPeople()
        {
            var repository = new Repository();
            var actual = repository.GetPeopleList();
            var expected = new List<Person>{new Person("Tiffany")};

            Assert.Equal(expected, actual);
        
        }
    }


}