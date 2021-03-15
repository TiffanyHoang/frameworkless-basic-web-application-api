using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace WebApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Run();
            //server.Stop();  // never reached...
        }
        static void Run()
        {
            var server = new HttpListener();
            server.Prefixes.Add("http://*:8080/");
            server.Start();
            Console.WriteLine("Server started");
            DateTimeManager dateTimeManager = new DateTimeManager();
            List<Person> peopleList = new List<Person> { new Person("Bob"), new Person("Tiff") };

            while (true)
            {
                var context = server.GetContext();  // Gets the request
                var request = context.Request;
                Console.WriteLine($"Requesting {request.HttpMethod} {request.Url.AbsolutePath}");

                Task.Run(() =>
                {
                    var peopleListString = "";
                    foreach (var people in peopleList)
                    {
                        peopleListString += people.Name + ' ';
                    }

                    if (request.HttpMethod == "GET" && request.Url.AbsolutePath == "/people")
                    {
                        var options = new JsonSerializerOptions { WriteIndented = true };
                        var buffer = JsonSerializer.SerializeToUtf8Bytes(peopleList, options);
                        context.Response.ContentLength64 = buffer.Length;
                        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    }

                    if (request.HttpMethod == "POST" && request.Url.AbsolutePath == "/people")
                    {
                        using (var reader = new StreamReader(request.InputStream,
                                     request.ContentEncoding))
                        {
                            var newPerson = reader.ReadToEnd();
                            var newPersonJson = JsonSerializer.Deserialize<Person>(newPerson);
                            peopleList.Add(new Person(newPersonJson.Name));
                            var status = System.Text.Encoding.UTF8.GetBytes($"Created {newPersonJson.Name}");

                            context.Response.ContentLength64 = status.Length;
                            context.Response.OutputStream.Write(status, 0, status.Length);
                        }
                    }


                    if (request.HttpMethod == "DELETE" && request.Url.AbsolutePath == "/people")
                    {
                        using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                        {


                            var deletePerson = reader.ReadToEnd();
                            var deletePersonJson = JsonSerializer.Deserialize<Person>(deletePerson);

                            var deletePersoninList = peopleList.Find(x => x.Name == deletePersonJson.Name);

                            peopleList.Remove(deletePersoninList);


                            var status = System.Text.Encoding.UTF8.GetBytes($"Deleted {deletePersoninList.Name}");
                            context.Response.ContentLength64 = status.Length;
                            context.Response.OutputStream.Write(status, 0, status.Length);
                        }
                    }

                    if (request.HttpMethod == "GET" && request.Url.AbsolutePath == "/")
                    {
                        var timeText = $"the time on the server is {dateTimeManager.GetCurrentTime()} on {dateTimeManager.GetCurrentDate()}";
                        var buffer = System.Text.Encoding.UTF8.GetBytes($"Hello {peopleListString} - {timeText}"); //construct string from peopleListS
                        context.Response.ContentLength64 = buffer.Length;
                        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                    }
                });
            }
        }
    }
}