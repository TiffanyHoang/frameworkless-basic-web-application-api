using System.Net;

namespace WebApplication.Server
{
    public interface IServer
    {
        void Run();
        void Stop();
        HttpListenerContext Context();
    }
}