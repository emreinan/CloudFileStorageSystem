using CloudFileStorageMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CloudFileStorageMVC.Controllers
{
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
            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Title = TempData["ErrorTitle"]?.ToString() ?? "An error occurred",
                Message = TempData["ErrorMessage"]?.ToString() ?? "An unexpected error occurred while processing your request.",
                StatusCode = TempData["ErrorCode"] != null ? int.Parse(TempData["ErrorCode"].ToString()!) : 500
            };

            return View(errorViewModel);
        }

    }
}
