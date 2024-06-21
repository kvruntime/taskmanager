using Microsoft.EntityFrameworkCore;
using TaskManager.Shared;

namespace TaskManager.Server.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaskItem> TaskItems { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
    }
}