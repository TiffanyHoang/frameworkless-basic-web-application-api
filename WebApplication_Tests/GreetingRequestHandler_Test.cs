// [Fact]
        // public void HandleGreeting_ReturnCorrectGreetingMessage()
        // {
        //     var request = Mock.Of<IRequest>();
        //     var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
        //     var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);
        //     _requestHandler.HandleRequest(context);
        //     var time = DateTime.Now.ToString("HH:mm");
        //     var date = DateTime.Now.ToString("dd MMM yyyy");
        //     var expectedResponse = $"Hello Tiffany - the time on the server is {time} on {date}";
        //     Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        //     response.OutputStream.Position = 0;
        //     var actualResponse = new StreamReader(response.OutputStream, Encoding.UTF8).ReadToEnd();
        //     Assert.Equal(expectedResponse, actualResponse);
        // }