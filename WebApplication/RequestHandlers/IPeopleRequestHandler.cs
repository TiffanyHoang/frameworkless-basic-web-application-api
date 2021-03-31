using WebApplication.Http;

namespace WebApplication.RequestHandlers
{
    public interface IPeopleRequestHandler
    {
        void HandleRequest(IContext context);
    }
}