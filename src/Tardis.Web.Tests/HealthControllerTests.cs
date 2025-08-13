// <copyright file="HealthControllerTests.cs" company="Tardis">
// Copyright © 2025 Tardis
// </copyright>
namespace Tardis.Web.Tests
{
    using Microsoft.AspNetCore.Mvc;
    using Tardis.Web.Controllers;
    using Xunit;

    /// <summary>
    /// Tests for the HealthController.
    /// </summary>
    public class HealthControllerTests
    {
        /// <summary>
        /// Verifies that GET /health returns 200 OK with a payload containing status = "ok".
        /// </summary>
        [Fact]
        public void Get_Returns_Ok_With_StatusOk()
        {
            var controller = new HealthController();
            var result = controller.Get();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(ok.Value);
            var statusProp = ok.Value.GetType().GetProperty("status");
            Assert.NotNull(statusProp);
            Assert.Equal("ok", statusProp!.GetValue(ok.Value)?.ToString());
        }
    }
}
