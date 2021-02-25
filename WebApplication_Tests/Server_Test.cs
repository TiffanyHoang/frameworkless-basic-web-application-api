using Xunit;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace WebApplication_Tests
{
    public class Server_Test
    {
        // private static readonly HttpClient client = new HttpClient();


        // [Fact]
        // public async void test()
        // {
        //     Console.WriteLine("a1");
        //     var responseString = await client.GetStringAsync("http://localhost:8080/people");
        //     Console.WriteLine("a2");

        //     Assert.Equal("Hello", responseString);
        //     Console.WriteLine("a3");
        // }

        // [Fact]
        // public async void test2()
        // {
        //     Console.WriteLine("b1");
        //     var responseString = await client.GetStringAsync("http://localhost:8080/people");
        //     Console.WriteLine("b2");

        //     Assert.Equal("Hello", responseString);
        //     Console.WriteLine("b3");

        // }

        [Fact]
        public async void test()
        {
            HttpClient client = new HttpClient();
            Console.WriteLine("a1");
            var responseString = await client.GetStringAsync("http://localhost:8080/people");
            Console.WriteLine("a2");

            Assert.Equal("Hello", responseString);
            Console.WriteLine("a3");
        }

        [Fact]
        public async void test2()
        {
            HttpClient client = new HttpClient();
            Console.WriteLine("b1");
            var responseString = await client.GetStringAsync("http://localhost:8080/people");
            Console.WriteLine("b2");

            Assert.Equal("Hello", responseString);
            Console.WriteLine("b3");

        }

        // [Fact]
        // public async void testPost()
        // {
        //     var values = new Dictionary<string, string>
        //         {
        //             { "Name", "hello" },
        //         };

        //     string json = JsonConvert.SerializeObject(values, Formatting.Indented);
        //     var content = new StringContent(json);
        //     Console.WriteLine("b1");
        //     var response = await client.PostAsync("http://localhost:8080/people", content);
        //     Console.WriteLine("b2");

        //     // var responseString = await response.Content.ReadAsStringAsync();
        //     Console.WriteLine("b3");

        //     Assert.Equal("Created hello", response.Content.ToString());
        //     Console.WriteLine("b4");
        // }
    }
}

