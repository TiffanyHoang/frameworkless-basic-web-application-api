using Xunit;
using WebApplication.RequestHandlers;
using WebApplication;
using System.IO;
using Moq;
using WebApplication.Repositories;
using System;

namespace WebApplication_Tests
{
    public class RouteController_Test
    {
        private readonly Repository _repository;
        public RouteController_Test()
        {
            _repository = new Repository();
        }
        [Fact]
        public void RequestRouter_Root_ShouldCallGreetingRequestHandler()
        {
            var request = Mock.Of<IRequest>(r => r.Url == new Uri("http://localhost:8080/"));
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            var greetingRequestHandler = new Mock<IGreetingRequestHandler>();

            IPeopleRequestHandler peopleRequestHandler = new PeopleRequestHandler(_repository);

            RouteController routeController = new RouteController(_repository, greetingRequestHandler.Object, peopleRequestHandler);

            routeController.RequestRouter(context);

            greetingRequestHandler.Verify(x => x.HandleRequest(context), Times.Once);
        }

        [Fact]
        public void RequestRouter_PepplePath_ShouldCallPeopleRequestHandler()
        {
            var request = Mock.Of<IRequest>(r => r.Url == new Uri("http://localhost:8080/people"));
            var response = Mock.Of<IResponse>(r => r.OutputStream == new MemoryStream());
            var context = Mock.Of<IContext>(c => c.Request == request && c.Response == response);

            IGreetingRequestHandler greetingRequestHandler = new GreetingRequestHandler(_repository);

            var peopleRequestHandler = new Mock<IPeopleRequestHandler>();

            RouteController routeController = new RouteController(_repository, greetingRequestHandler, peopleRequestHandler.Object);

            routeController.RequestRouter(context);

            peopleRequestHandler.Verify(x => x.HandleRequest(context), Times.Once);
        }
    }
}
