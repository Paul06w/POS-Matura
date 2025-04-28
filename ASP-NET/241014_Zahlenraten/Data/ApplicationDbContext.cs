using _241014_Zahlenraten.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _241014_Zahlenraten.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Score> HighScores { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
