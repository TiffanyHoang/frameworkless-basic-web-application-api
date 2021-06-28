using System;
using System.Linq;
using WebApplication.Http;
using System.Collections.Generic;

namespace WebApplication.RequestHandlers
{
    public static class Authentication
    {
        public static bool ValidateAuthentication(IRequest request)
        {
            var headers = request.Headers;
            if (headers == null)
            {
                return false;
            }
            var headerKeys = headers.Keys.OfType<string>();
            List<string> stringHeaderKeys = headerKeys.Select(s => (string)s.ToLower()).ToList();
            var isAuthorizationKeyAdded = stringHeaderKeys.Contains("Authorization".ToLower());
            if (!isAuthorizationKeyAdded)
            {
                return false;
            }
            var secret = Environment.GetEnvironmentVariable("SECRET");
            string[] values = headers.GetValues("Authorization");
            var isAuthorizationValueCorrect = values[0].ToString() == "Basic " + secret;
            return isAuthorizationKeyAdded && isAuthorizationValueCorrect;
        }
    }
}