using _250317_3PLF_Anmeldesystem.Models;
using Microsoft.EntityFrameworkCore;

namespace _250317_3PLF_Anmeldesystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
