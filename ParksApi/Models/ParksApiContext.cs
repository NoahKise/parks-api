using ParksApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ParksApi.Models
{
    public class ParksApiContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Park> Parks { get; set; }

        public ParksApiContext(DbContextOptions<ParksApiContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUserLogin<string>>(e =>
            {
                e.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });
            builder.Entity<Park>()
              .HasData(
                new Park { ParkId = 1, Name = "Milo McIver" },
                new Park { ParkId = 2, Name = "Nehalem Bay" },
                new Park { ParkId = 2, Name = "Oswald West" }
              );
        }
    }
}