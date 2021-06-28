using Xunit;
using WebApplication.RequestHandlers;
using WebApplication;
using System.IO;
using Moq;
using System;
using System.Net;
using WebApplication.Http;
using WebApplication.Services;

namespace WebApplication_Tests
{
    public class RouteController_Test
    {
        private readonly IPeopleService _peopleService;
        private IGreetingService _greetingService;
        public RouteController_Test()
        {
            _peopleService = Mock.Of<IPeopleService>();
            _greetingService = Mock.Of<IGreetingService>(); 

        }
        [Fact]
        public void RequestRouter_Root_ShouldCallGreetingRequestHandler()
        {
            var request = Mock.Of<IRequest>(r => r.Url == new Uri("http://localhost:8080/"));
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            var greetingRequestHandler = new Mock<IRequestHandler>();

            IRequestHandler peopleRequestHandler = new PeopleRequestHandler(_peopleService);
            
            IRequestHandler healthCheckHandler = new HealthCheckHandler();

            RouteController routeController = new RouteController( greetingRequestHandler.Object, peopleRequestHandler, healthCheckHandler);

            routeController.RequestRouter(context);

            greetingRequestHandler.Verify(x => x.HandleRequest(context), Times.Once);
        }

        [Fact]
        public void RequestRouter_PeoplePath_ShouldCallPeopleRequestHandler()
        {
            var request = Mock.Of<IRequest>(r => r.Url == new Uri("http://localhost:8080/people"));
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            IRequestHandler greetingRequestHandler = new GreetingRequestHandler(_greetingService);

            var peopleRequestHandler = new Mock<IRequestHandler>();
            
            IRequestHandler healthCheckHandler = new HealthCheckHandler();

            RouteController routeController = new RouteController(greetingRequestHandler, peopleRequestHandler.Object, healthCheckHandler);

            routeController.RequestRouter(context);

            peopleRequestHandler.Verify(x => x.HandleRequest(context), Times.Once);
        }

        [Fact]
        public void RequestRouter_InvalidPath_ReturnNotFoundStatus()
        {
            var request = Mock.Of<IRequest>(r => r.Url == new Uri("http://localhost:8080/Invalid"));
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            IRequestHandler greetingRequestHandler = new GreetingRequestHandler(_greetingService);

            IRequestHandler peopleRequestHandler = new PeopleRequestHandler(_peopleService);

            IRequestHandler healthCheckHandler = new HealthCheckHandler();

            RouteController routeController = new RouteController(greetingRequestHandler, peopleRequestHandler, healthCheckHandler);

            routeController.RequestRouter(context);

            Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
