using Dapper;
using MVCWEB.DAL.Abstract;
using MVCWEB.Data;
using MVCWEB.Models;
using MVCWEB.Models.Entities;

namespace MVCWEB.DAL
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DapperContext _dapperContext;
        public ProjectRepository(
            DapperContext dapper
            ) {
            _dapperContext = dapper;
        }
        public async Task<PaginatedResult<Project>> BrowseAllProjects(int page, int pageSize, string? search)
        {
            using var conn = _dapperContext.CreateConnection();
            var OFFSET = (page - 1) * pageSize;

            var query = @"
                SELECT p.Project_id, p.Title, p.Description, p.Status, p.CreatedAt,
                STRING_AGG(c.Category_name, ', ') AS CategoryNames
                FROM Project p

                LEFT JOIN ProjectCategories pc ON pc.Project_id = p.Project_id

                LEFT JOIN Categories c ON c.Category_id = pc.Category_id
                WHERE (@Search IS NULL
                OR p.Title LIKE '%' + @Search + '%'
                OR p.Description LIKE '%' + @Search + '%')
                GROUP BY p.Project_id, p.Title, p.Description, p.Status, p.CreatedAt
                ORDER BY p.CreatedAt DESC
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

                SELECT COUNT(*) FROM Project
                WHERE (@Search IS NULL
                OR Title LIKE '%' + @Search + '%'
                OR Description LIKE '%' + @Search + '%');";

            using var multi = await conn.QueryMultipleAsync(query, new
            { Offset = OFFSET, PageSize = pageSize, Search = search  }
            );
            var items = (await multi.ReadAsync<Project>()).ToList();
            var total = await multi.ReadFirstAsync<int>();

            return new PaginatedResult<Project>() { 
                Items = items,
                TotalCount = total,
                Page = page,
                PageSize = pageSize,
            };
        }

        public Task CreateProject(int userId, Project project)
        {
            throw new NotImplementedException();
        }

        public Task DisposeProject(int ownerId, int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedResult<Project>> GetJoinedProjects(int userId, int page, int pageSize, string? search)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedResult<Project>> GetOwnedProjects(int userId, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Project?> GetByIdAsync(int projectId)
        {
            using var conn = _dapperContext.CreateConnection();
            string query = @"
                SELECT p.Project_id, p.Title, p.Description, p.Status, p.CreatedAt, p.Owner_id,
                STRING_AGG(c.Category_name, ', ') AS CategoryNames,
                CONCAT(u.FirstName,' ', u.LastName) as OwnerName, 
                COUNT(DISTINCT tm.User_id) AS TotalMembers
                FROM Project p

                LEFT JOIN ProjectCategories pc ON pc.Project_id = p.Project_id
                LEFT JOIN Categories c ON c.Category_id = pc.Category_id
                LEFT JOIN TeamMembers tm ON tm.Project_id = p.Project_id
                LEFT JOIN Users u ON u.User_id = p.Owner_id 
                WHERE p.Project_id = @ProjectId
                GROUP BY p.Project_id, p.Title, p.Description, p.Status, p.CreatedAt, p.Owner_id,
                u.FirstName, u.LastName";
            
            return await conn.QueryFirstOrDefaultAsync<Project>(query, new { ProjectId = projectId });

        }

        public async Task<List<TeamMembers>> GetProjectTeamMembers(int projectId)
        {
            using var conn = _dapperContext.CreateConnection();
            conn.Open();

            string query = @"
            SELECT u.User_id AS User_id,
                   u.Email AS Email,
                   CONCAT(u.FirstName,' ', u.LastName) AS Fullname
            FROM TeamMembers AS tm
            INNER JOIN Users u ON u.User_id = tm.User_id
            WHERE tm.Project_id = @ProjectId";

            return (await conn.QueryAsync<TeamMembers>(query,
                new { ProjectId = projectId })).ToList();

        }
    }
}
