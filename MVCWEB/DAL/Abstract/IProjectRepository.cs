using MVCWEB.Models.Entities;

namespace MVCWEB.DAL.Abstract
{
    public interface IProjectRepository
    {
        Task CreateProject(int userId, Project project);
        Task DisposeProject(int OwnerId);
        Task<Project?> GetMainProject(int ProjectId);
        Task<bool> IsUserProjectMember(int UserId, int ProjectId);
        Task<bool> IsUserProjectOwner(int UserId, int ProjectId);
        Task<bool> IsUserInRequest(int UserId, int ProjectId);

        Task<bool> IsProjectFull(int ProjectId);

        Task RequestToJoin(JoinRequests request);
    }
}
