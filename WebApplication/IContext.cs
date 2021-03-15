using System;
using System.IO;
using System.Net;
using System.Text;

namespace WebApplication
{
    public interface IContext
    {
        public IRequest Request { get; }
        public IResponse Response { get; }
    }

    public interface IRequest
    {
        public Stream InputStream { get; }
        public Encoding ContentEncoding { get; }
        public Uri Url {get;}

    }

    public interface IResponse
    {
        public Stream OutputStream { get; }
        public long ContentLength64 { get; set; }
        public int StatusCode { get; set; }
    }
}