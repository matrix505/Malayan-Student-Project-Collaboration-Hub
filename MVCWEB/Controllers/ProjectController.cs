using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MVCWEB.DAL.Abstract;
using MVCWEB.Models.Entities;
using MVCWEB.ViewModel.ForProject;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCWEB.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _project;
        private readonly ICollabRepository _collab;
        private readonly ILogger<ProjectController> _logger;
        private readonly IDiscussionRepository _discussion;

        public ProjectController(
            IProjectRepository projectRepository,
            ICollabRepository collabRepository,
            ILogger<ProjectController> logger,
            IDiscussionRepository discussionRepository
            )
        {
            _project = projectRepository;
            _collab = collabRepository;
            _logger = logger;
            _discussion = discussionRepository;

        }
        [HttpGet]
        public async Task<IActionResult> Main(int id,string? tab = null)
        {
            
            var GetMainProject = await _project.GetMainProject(id);

            if(GetMainProject == null)
            {
                return NotFound();
            }

            var UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var MainProject = new ProjectMainViewModel()
            {
                Id = GetMainProject.Project_id,
                Title = GetMainProject.Title!,
                Description = GetMainProject.Description!,
                Categories = GetMainProject.CategoryNames!.Split(",").ToList(),
                MemberSize = GetMainProject.MemberSize,
                Status = GetMainProject.Status!,
                Members = await _collab.GetProjectTeamMembers(id),
                IsUserProjectMember = await _project.IsUserProjectMember(UserId,id),
                IsUserProjectOwner = await _project.IsUserProjectOwner(UserId, id)
                ,TotalMembers = GetMainProject.TotalMembers,
                CreatedAt = GetMainProject.CreatedAt,
                tab = tab

            };

            switch(tab) {
                case "discussions":
                    MainProject.DiscussionTopics = await _discussion.GetAllDiscussions(id);
                    break;
            }


            return View(MainProject);
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

        [HttpGet]
        public async Task<IActionResult> Requests(int ProjectId)
        {
            // TODO: return partial modal for list of join requests

            var joinRequests = await _project.ViewJoinRequests(ProjectId);
            if (joinRequests == null || !Request.Headers.ContainsKey("X-Requested-With"))
            {
                return NotFound();
            }

            var RequestList = new ProjectJoinRequestsViewModel()
            {
                UserJoinRequests = joinRequests
            };
         
            return PartialView("Partials/Project/_ProjectModalJoinRequestsPartial",RequestList);
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
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ResponseJoinRequest(int requestId, string action)
        {
            _logger.LogInformation(requestId.ToString());
            if (requestId <= 0)
                return Json(new { success = false, message = "Invalid request ID." });

            if (action != "accept" && action != "reject")
                return Json(new { success = false, message = "Invalid action." });

            bool result = false;

            switch (action)
            {
                case "accept":
                    result = await _project.AcceptJoinRequest(requestId);
                    break;
                case "reject":
                    result = await _project.RejectJoinRequest(requestId);
                    break;
            }

            return Json(new { success = result });
        }
        [HttpGet]
        public IActionResult Discussions()
        {
            // TODO : 
            return View();
        }

        [HttpGet]
        public IActionResult CreateDiscussion(int projectId)
        {
            return View(new ProjectCreateDiscussionViewModel() { ProjectId = projectId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDiscussion(ProjectCreateDiscussionViewModel pcd)
        {

            if(!ModelState.IsValid)
            {
                return View(pcd);
            }
            var create = new Discussions()
            {
                Project_id = pcd.ProjectId,
                Title = pcd.Title,
                Description = pcd.Description,
                CreatedAt = DateTime.Now,
                Creator_id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
            };
            //_logger.LogInformation(pcd.ProjectId.ToString());
            await _discussion.CreateDiscussion(create);

            return RedirectToAction("Main", new { id = pcd.ProjectId, tab = "discussions"});
        }

    }
}
