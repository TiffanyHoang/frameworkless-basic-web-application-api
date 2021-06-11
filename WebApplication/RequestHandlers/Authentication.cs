using System;
using WebApplication.Http;

namespace WebApplication.RequestHandlers
{
    public static class Authentication
    {
        public static bool ValidateAuthentication(IRequest request)
        {
            var secret = Environment.GetEnvironmentVariable("SECRET");
            var headers = request.Headers;        
            string[] values = headers.GetValues("Authorization");
            return values[0].ToString() == "Basic " + secret;
        }
    }
}