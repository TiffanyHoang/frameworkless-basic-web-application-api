using System;
using System.IO;
using System.Net;
using System.Text;

namespace WebApplication.Http
{
    public interface IContext
    {
        IRequest Request { get; }
        IResponse Response { get; }
    }

    public interface IRequest
    {
        Stream InputStream { get; }
        Encoding ContentEncoding { get; }
        Uri Url { get; }
        string HttpMethod { get; }
    }

    public interface IResponse
    {
        Stream OutputStream { get; }
        long ContentLength64 { get; set; }
        int StatusCode { get; set; }
    }
}