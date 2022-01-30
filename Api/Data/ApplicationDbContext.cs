using Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):
            base(options)
        {
        }

        public DbSet<Todotask> Todotasks { get; set; } = null!;
        public DbSet<Foldertask> Foldertasks { get; set; } = null!;

    }
}
