# Frameworkless Basic Web Application Kata

It's the late 1990s and the internet has just begun to flourish. You have a brilliant idea to build a Hello World web application however there are no MVC frameworks for you to use. Your mission is to implement a basic Hello World web application without using any frameworks. 

When calling the web application from a browser it should return a greeting with your name and the current date/time of the server. For instance, if your name was Bob when hitting the web server (http://localhost:8080) the browser would display "Hello Bob - the time on the server is 10:48pm on 14 March 2018" 

_(Yes, we know we said you were in the 1990s... but we want you to use your current servers date/time ;-))_

Oh, and of course you are one of those new age eXtreme programmers so you need to have appropriate tests! To make your life easier we have got starting points for Java and C#  

## To summarise 

* Keep with standard libraries, don't use frameworks like Spring/Dropwizard or equivalent  
* Try implement all the dispatching code manually.  
* Include in your solution a test case for proving that the "greeting" portion of the system is functioning correctly.  

## CSharp Starting Point

If you are working in C#, a trivial web server that doesn't rely on
ASP.NET Core follows. Note that it handles a single request at a time,
synchronously. Converting it to be asynchronous, so as to be able to handle
more than one request simultaneously, is a useful future exercise.

~~~
var server = new HttpListener();
server.Prefixes.Add("http://localhost:8080/");
server.Start();
while (true)
{
    var context = server.GetContext();  // Gets the request
    Console.WriteLine($"{context.Request.HttpMethod} {context.Request.Url}");
    var buffer = System.Text.Encoding.UTF8.GetBytes("Hello");
    context.Response.ContentLength64 = buffer.Length;
    context.Response.OutputStream.Write(buffer, 0, buffer.Length);  // forces send of response
}
server.Stop();  // never reached...
~~~

----------------------------------------------------------------------

