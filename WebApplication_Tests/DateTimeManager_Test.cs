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
            var actualFormat = DateTimeManager.GetCurrentTime();
            var expectedFormat = DateTime.Now.ToUniversalTime().ToString("HH:mm");
            Assert.Equal(expectedFormat, actualFormat);
        }

        [Fact]
        public void GivenDateTime_ReturnCorrectDateFormatddMMMyyyy()
        {
            var actualFormat = DateTimeManager.GetCurrentDate();
            var expectedFormat = DateTime.Now.ToUniversalTime().ToString("dd MMM yyyy");
            Assert.Equal(expectedFormat, actualFormat);
        }
    }
}

