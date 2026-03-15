using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVCWEB.Controllers
{
    [Authorize]
    public class CollabController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
