using Xunit;
using WebApplication.RequestHandlers;
using WebApplication;
using System.IO;
using Moq;
using WebApplication.Repositories;
using System;
using System.Net;
using WebApplication.Http;
using WebApplication.Services;

namespace WebApplication_Tests
{
    public class RouteController_Test
    {
        private PeopleService _peopleService;
        private GreetingService _greetingService;

        public RouteController_Test()
        {
            var repository = new Repository();
            _peopleService = new PeopleService(repository);
            _greetingService = new GreetingService(repository);

        }
        [Fact]
        public void RequestRouter_Root_ShouldCallGreetingRequestHandler()
        {
            var request = Mock.Of<IRequest>(r => r.Url == new Uri("http://localhost:8080/"));
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            var greetingRequestHandler = new Mock<IGreetingRequestHandler>();

            IPeopleRequestHandler peopleRequestHandler = new PeopleRequestHandler(_peopleService);

            RouteController routeController = new RouteController( greetingRequestHandler.Object, peopleRequestHandler);

            routeController.RequestRouter(context);

            greetingRequestHandler.Verify(x => x.HandleRequest(context), Times.Once);
        }

        [Fact]
        public void RequestRouter_PepplePath_ShouldCallPeopleRequestHandler()
        {
            var request = Mock.Of<IRequest>(r => r.Url == new Uri("http://localhost:8080/people"));
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            IGreetingRequestHandler greetingRequestHandler = new GreetingRequestHandler(_greetingService);

            var peopleRequestHandler = new Mock<IPeopleRequestHandler>();

            RouteController routeController = new RouteController( greetingRequestHandler, peopleRequestHandler.Object);

            routeController.RequestRouter(context);

            peopleRequestHandler.Verify(x => x.HandleRequest(context), Times.Once);
        }

        [Fact]
        public void RequestRouter_UnvalidPath_ReturnNotFoundStatus()
        {
            var request = Mock.Of<IRequest>(r => r.Url == new Uri("http://localhost:8080/Unvalid"));
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            IGreetingRequestHandler greetingRequestHandler = new GreetingRequestHandler(_greetingService);

            IPeopleRequestHandler peopleRequestHandler = new PeopleRequestHandler(_peopleService);

            RouteController routeController = new RouteController(greetingRequestHandler, peopleRequestHandler);

            routeController.RequestRouter(context);

            Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
