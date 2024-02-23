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
                new Park { ParkId = 1, Name = "Milo McIver", User = "noah@email.com", BeachAccess = false, Camping = true, DiscGolf = true, Kayaking = true },
                new Park { ParkId = 2, Name = "Nehalem Bay", User = "noah@email.com", BeachAccess = true, Camping = true, DiscGolf = false, Kayaking = true },
                new Park { ParkId = 3, Name = "Oswald West", User = "noah@email.com", BeachAccess = true, Camping = false, DiscGolf = false, Kayaking = false }
              );
        }
    }
}