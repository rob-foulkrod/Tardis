using Xunit;
using Tardis.Web.Controllers;
using Tardis.Web.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Tardis.Web.Tests
{
    public class ErrorViewModelTests
    {
        [Fact]
        public void ShowRequestId_ReturnsTrue_WhenRequestIdIsNotEmpty()
        {
            var model = new ErrorViewModel { RequestId = "123" };
            Assert.True(model.ShowRequestId);
        }

        [Fact]
        public void ShowRequestId_ReturnsFalse_WhenRequestIdIsEmpty()
        {
            var model = new ErrorViewModel { RequestId = "" };
            Assert.False(model.ShowRequestId);
        }
    }

    public class HomeControllerErrorTests
    {
        [Fact]
        public void Error_Returns_ViewResult_With_ErrorViewModel()
        {
            var loggerMock = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(loggerMock.Object);

            // Setup HttpContext with TraceIdentifier
            var httpContext = new DefaultHttpContext();
            httpContext.TraceIdentifier = "test-trace-id";
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };

            var result = controller.Error();
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<ErrorViewModel>(viewResult.Model);
        }
    }
}
