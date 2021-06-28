using WebApplication.Repositories;
using Serilog;
using System;
using System.Net.Http;

namespace WebApplication.Services
{
    public interface IGreetingService
    {
        string Greeting();
    }
}