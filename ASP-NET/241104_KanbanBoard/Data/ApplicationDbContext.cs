using _241104_KanbanBoard.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _241104_KanbanBoard.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<KanbanTask> Tasks { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
