﻿using System;
using System.Net;
using System.Threading.Tasks;

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

                Task.Run(async () => {
                    var name = request.QueryString.Get("name");
                    // var name = "Bob";
                    var timeText = $"the time on the server is {dateTimeManager.GetCurrentTime()} on {dateTimeManager.GetCurrentDate()}";
                    Console.WriteLine($"{context.Request.HttpMethod} {context.Request.Url}");
                    var buffer = System.Text.Encoding.UTF8.GetBytes($"Hello {name} - {timeText}");

                    // Send the response - prior to sending the response, create a delay between 0 - 500ms
                    await Task.Delay(new Random(5));
                    context.Response.ContentLength64 = buffer.Length;
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);  // forces send of response
                });
            }
            //server.Stop();  // never reached...
        }
    }
}
