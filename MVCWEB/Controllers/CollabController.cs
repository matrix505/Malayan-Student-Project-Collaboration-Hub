using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCWEB.DAL.Abstract;
using MVCWEB.ViewModel.Collab;
using System.Threading.Tasks;

namespace MVCWEB.Controllers
{
    [Authorize]
    public class CollabController : Controller
    {
        private readonly IProjectRepository _project;

        public CollabController(
              IProjectRepository project)
        {
            _project = project;
        }
        public IActionResult Index(
          
            )
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> BrowseProjects(int page =1, string? search = null)
        {
            int pageSize = 20; // 4 x 5;
            
            var projects = new CollabViewModel()
            {
                Projects = await _project.BrowseAllProjects(page, pageSize, search),
                Search = search
            };
            return View(projects);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var project = new CollabViewModel() { Project = await _project.GetByIdAsync(id) };

            if(project is null || !Request.Headers.ContainsKey("X-Requested-With"))
            {
                return NotFound();
            }

            project.Members = await _project.GetProjectTeamMembers(id);
            

            return PartialView("Partials/_ProjectModalViewPartial",project);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult JoinRequests()
        {   
            return View();
        }

    }
}
