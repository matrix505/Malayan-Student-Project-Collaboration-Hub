using MVCWEB.Models;
using MVCWEB.Models.Entities;

namespace MVCWEB.ViewModel.Collab
{
    public class CollabViewModel
    {
        public PaginatedResult<Project> Projects { get; set; } = new();
        public List<TeamMembers> Members { get; set; } = new();

        public Project? Project { get; set; }

        public bool isProjectMember { get; set; } = false;
        public bool isOwnerMember { get; set; } = false;
        public bool isRequested { get; set; } = false;
        
        public string? Search {  get; set; }


        public bool isSearchAllowed { get; set; } = true;
        public bool isPaginationAllowed { get; set; } = true;
    }
}
