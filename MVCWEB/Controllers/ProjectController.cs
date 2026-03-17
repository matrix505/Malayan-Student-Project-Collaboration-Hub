using Microsoft.AspNetCore.Mvc;
using MVCWEB.DAL.Abstract;
using MVCWEB.Models.Entities;
using MVCWEB.ViewModel.ForProject;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCWEB.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _project;
        private readonly ICollabRepository _collab;

        public ProjectController(
            IProjectRepository projectRepository,
            ICollabRepository collabRepository
            )
        {
            _project = projectRepository;
            _collab = collabRepository;
        }
        [HttpGet]
        public IActionResult Main(int id,string tab = "overview")
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var CreateProject = new ProjectCreateViewModel()
            {
                Skills = await _collab.GetAllAvailableSkills(),
                Categories = await _collab.GetAllAvailableCategories()
            };
            return View(CreateProject);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectCreateViewModel ccvm)
        {
            ccvm.Skills = await _collab.GetAllAvailableSkills();
            ccvm.Categories = await _collab.GetAllAvailableCategories();

            if (!ModelState.IsValid)
            {
                return View(ccvm);
            }

            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var NewProject = new Project()
            {
                Title = ccvm.Title,
                Description = ccvm.Description,
                MemberSize = ccvm.MemberSize,

                Categories = ccvm.SelectedCategoryIds!.
                Select(id => new ProjectCategories
                {
                    Category_id = int.Parse(id),

                }).ToList()
                ,
                Skills = ccvm.SelectedSkillIds!.
                Select(id => new ProjectSkills
                {
                    Skill_id = id

                }).ToList(),
            };

            await _project.CreateProject(userId, NewProject);

            return RedirectToAction("Index", "Dashboard");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestToJoin(int projectId)
        {
            
            var Request = new JoinRequests()
            {
                Project_id = projectId,
                User_id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!),
            };
            await _project.RequestToJoin(Request);
            return RedirectToAction("Index","Home");
        }

    }
}
