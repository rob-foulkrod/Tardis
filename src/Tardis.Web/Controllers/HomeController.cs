using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tardis.Web.Models;

namespace Tardis.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
            string requestId = Activity.Current?.Id;
            if (string.IsNullOrEmpty(requestId) && HttpContext != null)
            {
                requestId = HttpContext.TraceIdentifier;
            }
            return View(new ErrorViewModel { RequestId = requestId ?? string.Empty });
    }
}
