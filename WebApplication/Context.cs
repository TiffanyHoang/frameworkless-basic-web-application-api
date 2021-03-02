using System.IO;
using System.Net;
using System.Text;

namespace WebApplication
{

public class Context : IContext
    {
        public Context(HttpListenerContext context)
        {
            Request = new Request(context.Request);
            Response = new Response(context.Response);
        }

        public IRequest Request { get; }
        public IResponse Response { get; }

        public void Close()
        {
            Response.OutputStream.Close();
        }
    }

    public class Request : IRequest
    {
        private readonly HttpListenerRequest _request;
        
        public Request(HttpListenerRequest request)
        {
            _request = request;
        }
        
        public Stream InputStream => _request.InputStream;
        public Encoding ContentEncoding => _request.ContentEncoding;
    }

    public class Response : IResponse
    {
        private readonly HttpListenerResponse _response;
        
        public Response(HttpListenerResponse response)
        {
            _response = response;
        }

        public long ContentLength64
        {
            get => _response.ContentLength64;
            set => _response.ContentLength64 = value;
        }
        
        public int StatusCode
        {
            get => _response.StatusCode;
            set => _response.StatusCode = value;
        }

        public Stream OutputStream => _response.OutputStream;
    }
}