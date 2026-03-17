using Microsoft.AspNetCore.Mvc;

namespace MVCWEB.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Main()
        {
            return View();
        }
    }
}
