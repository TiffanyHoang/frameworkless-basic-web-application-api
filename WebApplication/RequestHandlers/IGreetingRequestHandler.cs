using WebApplication.Http;

namespace WebApplication.RequestHandlers
{
    public interface IGreetingRequestHandler
    {
        void HandleRequest(IContext context);
    }
}
