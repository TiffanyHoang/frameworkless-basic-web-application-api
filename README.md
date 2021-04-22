# Frameworkless Web Application Kata
[![Build status](https://badge.buildkite.com/c68e56a72e0d1c72022f8e8e91d9506935c7d0658b7f62f135.svg)](https://buildkite.com/myob/tiffany-frameworkless-web-app-api)

A .NET Core solution to [the Frameworkless Basic Web Application kata](https://github.com/MYOB-Technology/General_Developer/blob/main/katas/kata-frameworkless-basic-web-application/kata-frameworkless-basic-web-application.md) with [enhancements](https://github.com/MYOB-Technology/General_Developer/blob/main/katas/kata-frameworkless-basic-web-application/kata-frameworkless-basic-web-application-enhancements.md).

## Endpoints
These are the supported endpoints along with example responses. Use your favourite HTTP client, such as Postman or REST client, to make requests!

## GET / 
Return greeting message  
Request   
``GET /``  
Respond  
~~~
HTTP/1.1 200 OK
Server: Microsoft-NetCore/2.0
Date: Tue, 06 Apr 2021 00:05:40 GMT
Content-Length: 62
Connection: close

Hello Tiffany - the time on the server is 10:05 on 06 Apr 2021
~~~

## GET /people
Get all people in the data   
Request  
``GET /people``  
Respond  
~~~
HTTP/1.1 200 OK
Server: Microsoft-NetCore/2.0
Date: Tue, 06 Apr 2021 00:09:26 GMT
Content-Length: 33
Connection: close

[
  {
    "Name": "Tiffany"
  }
]
~~~

## POST /people
Create a new person in body content    
Request  
``POST /people``  
Raw body content  
``
{
    "Name":"DS"
}  
``  
Respond  
~~~
HTTP/1.1 200 OK
Server: Microsoft-NetCore/2.0
Date: Tue, 06 Apr 2021 00:17:30 GMT
Content-Length: 18
Connection: close

{
  "Name": "DS"
}
~~~

## PUT /people/{oldPerson}
Update a person name     
Request  
``PUT /people/DS``  
New person name in raw body content   
``
{
    "Name":"DSTeoh"
}
``
~~~
HTTP/1.1 200 OK
Server: Microsoft-NetCore/2.0
Date: Tue, 06 Apr 2021 00:20:19 GMT
Content-Length: 22
Connection: close

{
  "Name": "DSTeoh"
}
~~~

## DELETE /people/{person}
Delete a person    
Request    
``DELETE /people/Mattias``   
Respond   
~~~
HTTP/1.1 200 OK
Server: Microsoft-NetCore/2.0
Date: Tue, 06 Apr 2021 00:21:30 GMT
Connection: close
Transfer-Encoding: chunked
~~~

## Invalid path 
Request  
``GET /invalidPath``  
Respond  
~~~
HTTP/1.1 404 Not Found
Server: Microsoft-NetCore/2.0
Date: Tue, 06 Apr 2021 00:43:42 GMT
Connection: close
Transfer-Encoding: chunked
~~~  

## Invalid Method on Root
Request  
``POST /``  
Respond  
~~~
HTTP/1.1 405 Method Not Allowed
Server: Microsoft-NetCore/2.0
Date: Tue, 06 Apr 2021 00:46:18 GMT
Connection: close
Transfer-Encoding: chunked
~~~  

## Invalid Method on /people
Request  
``PATCH /people``  
Respond  
~~~
HTTP/1.1 405 Method Not Allowed
Server: Microsoft-NetCore/2.0
Date: Tue, 06 Apr 2021 00:48:10 GMT
Connection: close
Transfer-Encoding: chunked
~~~

