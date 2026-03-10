using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCWEB.DTOs;
using MVCWEB.Models;

namespace MVCWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        

        public HomeController(
            ILogger<HomeController> logger
            )
        {
            _logger = logger;
        }
        [HttpPost]
        public IActionResult Create(ItemDto item)
        {
            if (!ModelState.IsValid)
            {
                // Get all error messages
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                // Example: display or log
                foreach (var error in errors)
                {
                    _logger.LogInformation(error);
                }
            }
                return View("Index");
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
