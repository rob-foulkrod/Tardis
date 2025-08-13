// <copyright file="HomeController.cs" company="Tardis">
// Copyright © 2025 Tardis
// </copyright>
namespace Tardis.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using Tardis.Web.Models;

    /// <summary>
    /// Controller for home and privacy pages.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Returns the Index view.
        /// </summary>
        /// <returns>The Index view.</returns>
        public IActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Returns the Privacy view.
        /// </summary>
        /// <returns>The Privacy view.</returns>
        public IActionResult Privacy()
        {
            return this.View();
        }

        /// <summary>
        /// Returns the Error view.
        /// </summary>
        /// <returns>The Error view.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            string? requestId = Activity.Current?.Id;
            if (string.IsNullOrEmpty(requestId) && this.HttpContext != null)
            {
                requestId = this.HttpContext.TraceIdentifier;
            }
            return this.View(new ErrorViewModel { RequestId = requestId ?? string.Empty });
        }

    }
}
