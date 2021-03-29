using System;
using System.Net;
using WebApplication.Repositories;

namespace WebApplication.RequestHandlers
{
    public interface IGreetingRequestHandler
    {
        void HandleRequest(IContext context);
    }
}
