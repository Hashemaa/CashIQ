using System.Diagnostics;
using CashIQ.Models;
using Microsoft.AspNetCore.Mvc;

namespace CashIQ.Controllers
{
    public class DashboardController(ILogger<DashboardController> logger) : Controller
    {
        private readonly ILogger<DashboardController> _logger = logger;

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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
