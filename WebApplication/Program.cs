using System;
using System.Net;
using System.Threading.Tasks;
using WebApplication.Repositories;
using WebApplication.RequestHandlers;

namespace WebApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            while (true) {
                server.ProcessRequest();
            }
        }
    }
}