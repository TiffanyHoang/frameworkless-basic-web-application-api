using Xunit;
using WebApplication.RequestHandlers;
using Moq;
using WebApplication.Http;
using System.Collections.Specialized;
using System;

namespace WebApplication_Tests
{
    public class Authentication_Test
    {
        [Fact]
        public void ValidValue_ReturnTrue()
        {
            var secret = Environment.GetEnvironmentVariable("SECRET");
            var headers = new NameValueCollection();
            headers.Add("Authorization", $"Basic " + secret);
            var request = Mock.Of<IRequest>(r => r.Headers == headers);
            var actual = Authentication.ValidateAuthentication(request);
            Assert.True(actual);
        }

        [Fact]
        public void InvalidValue_ReturnFalse()
        {
            var headers = new NameValueCollection();
            headers.Add("Authorization", "invalidValue");
            var request = Mock.Of<IRequest>(r => r.Headers == headers);
            var actual = Authentication.ValidateAuthentication(request);
            Assert.False(actual);
        }

        [Fact]
        public void NoAuthorizationKey_ReturnFalse()
        {
            var headers = new NameValueCollection();
            headers.Add("", "");
            var request = Mock.Of<IRequest>(r => r.Headers == headers);
            var actual = Authentication.ValidateAuthentication(request);
            Assert.False(actual);
        }   
    }

}
