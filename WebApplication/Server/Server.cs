using System.Net;

namespace WebApplication.Server
{
    public class Server
    {
        private HttpListener _server;
        public Server(string prefixes)
        {
            _server = new HttpListener();
            _server.Prefixes.Add(prefixes);
        }
        public void Run()
        {
            _server.Start();
        }
        public void Stop()
        {
            _server.Close();
        }
        public HttpListenerContext Context()
        {
            return _server.GetContext();
        }
    }
}