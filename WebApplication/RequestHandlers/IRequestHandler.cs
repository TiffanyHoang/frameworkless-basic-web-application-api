using WebApplication.Http;

namespace WebApplication.RequestHandlers
{
    public interface IRequestHandler
    {
        void HandleRequest(IContext context);
    }
}