using _241125_Speisekarte.Models;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

namespace _241125_Speisekarte.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Speise> Speisen { get; set; }
        public DbSet<Zutat> Zutaten { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
