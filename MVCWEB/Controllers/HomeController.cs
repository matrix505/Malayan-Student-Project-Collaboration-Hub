using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCWEB.DAL.Abstract;
using MVCWEB.Data;
using MVCWEB.Enums;
using MVCWEB.ViewModel;

namespace MVCWEB.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICollabRepository _projectRepository;

        public HomeController(
            ILogger<HomeController> logger,
            ICollabRepository projectRepository)
        {
            _logger = logger;
            _projectRepository = projectRepository;
         
        }
       
        public IActionResult Index()
        {   
            
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Profile","Account");
            }
            return View();
        }
        public IActionResult Browse()
        {
            return View();
        }
        public IActionResult About()
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

