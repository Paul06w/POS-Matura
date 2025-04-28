using _241104_Zahlenraten02.Models;
using Microsoft.EntityFrameworkCore;

namespace _241104_Zahlenraten02.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Score> Scores { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            
        }
    }
}
