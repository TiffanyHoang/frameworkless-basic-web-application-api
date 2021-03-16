using System;
using Xunit;
using WebApplication;

namespace WebApplication_Tests
{
    public class DateTimeManager_Test
    {
        [Fact]
        public void GivenDateTime_ReturnCorrectTimeFormatHHmm()
        {
            DateTimeManager dateTimeManager = new DateTimeManager();
            var actualFormat = dateTimeManager.GetCurrentTime();
            var expectedFormat = DateTime.Now.ToString("HH:mm");
            Assert.Equal(expectedFormat, actualFormat);
        }

        [Fact]
        public void GivenDateTime_ReturnCorrectDateFormatddMMMyyyy()
        {
            DateTimeManager dateTimeManager = new DateTimeManager();
            var actualFormat = dateTimeManager.GetCurrentDate();
            var expectedFormat = DateTime.Now.ToString("dd MMM yyyy");
            Assert.Equal(expectedFormat, actualFormat);
        }
    }
}

