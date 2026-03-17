using MVCWEB.Models;
using MVCWEB.Models.Entities;

namespace MVCWEB.DAL.Abstract
{
    public interface ICollabRepository
    {
        Task<PaginatedResult<Project>> BrowseAllProjects(int page, int pageSize, string? search);
        Task<PaginatedResult<Project>> GetOwnedProjects(int userId,int page, int pageSize, string? search);
        Task<PaginatedResult<Project>> GetJoinedProjects(int userId,int page, int pageSize, string? search);
        

        Task<Project> GetByIdAsync(int projectId);
        Task<List<TeamMembers>> GetProjectTeamMembers(int projectId);

        Task<List<Categories>> GetAllAvailableCategories();
        Task<List<Skills>> GetAllAvailableSkills();

    }
}
