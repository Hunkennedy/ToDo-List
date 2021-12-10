using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):
            base(options)
        {
        }

        public DbSet<Todotask> Todotasks { get; set; } = null!;
        public DbSet<Foldertask> Foldertasks { get; set; } = null!;

    }
}
