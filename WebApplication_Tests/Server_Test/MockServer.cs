using System.Net;
using WebApplication.Server;

namespace WebApplication_Tests.Server_Test
{
    public class MockServer:IServer
    {
        private HttpListener _server;
        public MockServer(string prefixes)
        {
            _server = new HttpListener();
            _server.Prefixes.Add(prefixes);
            Run();
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