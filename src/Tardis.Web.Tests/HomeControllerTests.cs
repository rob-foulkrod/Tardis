using Xunit;
using Tardis.Web.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Tardis.Web.Tests
{
    public class HomeControllerTests
    {
        private HomeController CreateController()
        {
            var loggerMock = new Mock<ILogger<HomeController>>();
            return new HomeController(loggerMock.Object);
        }

        [Fact]
        public void Index_Returns_ViewResult()
        {
            var controller = CreateController();
            var result = controller.Index();
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Privacy_Returns_ViewResult()
        {
            var controller = CreateController();
            var result = controller.Privacy();
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Error_Returns_ViewResult_With_ErrorViewModel_TraceIdentifier()
        {
            var controller = this.CreateController();
            var httpContext = new DefaultHttpContext();
            httpContext.TraceIdentifier = "trace-123";
            controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = controller.Error();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Tardis.Web.Models.ErrorViewModel>(viewResult.Model);
            Assert.Equal("trace-123", model.RequestId);
            Assert.True(model.ShowRequestId);
        }

        [Fact]
        public void Error_Returns_ViewResult_With_ErrorViewModel_ActivityNull()
        {
            var controller = this.CreateController();
            var httpContext = new DefaultHttpContext();
            httpContext.TraceIdentifier = string.Empty;
            controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
            var result = controller.Error();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Tardis.Web.Models.ErrorViewModel>(viewResult.Model);
            Assert.Equal(string.Empty, model.RequestId);
            Assert.False(model.ShowRequestId);
        }

        [Fact]
        public void Error_Returns_ViewResult_With_ErrorViewModel_NoHttpContext()
        {
            var controller = this.CreateController();
            // No HttpContext set
            var result = controller.Error();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Tardis.Web.Models.ErrorViewModel>(viewResult.Model);
            Assert.True(model.RequestId == null || model.RequestId == string.Empty);
        }
    }

    public class SampleTests
    {
        [Fact]
        public void Addition_Works()
        {
            Assert.Equal(4, 2 + 2);
        }

        [Theory]
        [InlineData(2, 2, 4)]
        [InlineData(3, 3, 6)]
        public void Addition_Theory(int a, int b, int expected)
        {
            Assert.Equal(expected, a + b);
        }
    }
}
