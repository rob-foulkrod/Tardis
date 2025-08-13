// <copyright file="HealthController.cs" company="Tardis">
// Copyright © 2025 Tardis
// </copyright>
namespace Tardis.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Provides a simple health check endpoint for service availability.
    /// </summary>
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Gets a simple health response.
        /// </summary>
        /// <returns>HTTP 200 OK with a simple status payload.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(new { status = "ok" });
        }
    }
}
