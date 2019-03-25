using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Project_Untitled.Models
{
    public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }

        public DbSet<UserHandler> UserHandler { get; set; }
        public DbSet<Notifications> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Notifications>()
                .HasKey(a => a.Id);

            builder.Entity<UserHandler>()
                .ToTable("Users")
                .HasOne(a => a.Notifications)
                .WithOne()
                .HasForeignKey<Notifications>(b => b.UserId)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
        }
    }
}