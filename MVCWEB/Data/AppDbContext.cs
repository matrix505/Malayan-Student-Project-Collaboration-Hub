using Microsoft.EntityFrameworkCore;

namespace MVCWEB.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // Nothing's here
        }
       
    }
}
