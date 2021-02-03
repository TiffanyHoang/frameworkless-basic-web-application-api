using System;
using System.Net;

namespace WebApplication
{
    class DateTimeController
    {
        static void Main(string[] args)
        {
            var server = new HttpListener();
            server.Prefixes.Add("http://localhost:8080/");
            server.Start();
            Console.WriteLine("Server started");
            DateTimeManager dateTimeManager = new DateTimeManager(); 
            while (true)
            {
                var context = server.GetContext();  // Gets the request
                var request = context.Request;
                // var name = request.QueryString.Get("name");
                var name = "Bob";
                var timeText = $"the time on the server is {dateTimeManager.GetCurrentTime()} on {dateTimeManager.GetCurrentDate()}";
                Console.WriteLine($"{context.Request.HttpMethod} {context.Request.Url}");
                var buffer = System.Text.Encoding.UTF8.GetBytes($"Hello {name} - {timeText}");
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);  // forces send of response
            }
            //server.Stop();  // never reached...
        }
    }
}
