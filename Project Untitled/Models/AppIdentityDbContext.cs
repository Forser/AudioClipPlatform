using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Project_Untitled.Models
{
    public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }

        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Clips> Clips { get; set; }
        public DbSet<Liked> Likes { get; set; }
        public DbSet<Following> Followings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var messageConverter = new EnumToStringConverter<MessageStatus>();
            var fileConverter = new EnumToStringConverter<FileStatus>();

            base.OnModelCreating(builder);

            builder.Entity<Notifications>()
                .HasKey(a => a.Id);

            builder.Entity<UserSettings>()
                .HasKey(a => a.Id);

            builder.Entity<Message>()
                .Property(e => e.SenderStatus)
                .HasConversion(messageConverter);

            builder.Entity<Message>()
                .Property(e => e.RecipentStatus)
                .HasConversion(messageConverter);

            builder.Entity<Clips>()
                .Property(e => e.FileStatus)
                .HasConversion(fileConverter);

            builder.Entity<Liked>()
                .HasOne<Clips>(s => s.Clips)
                .WithMany(g => g.Likes)
                .HasForeignKey(s => s.ClipId);

            builder.Entity<Clips>()
                .HasOne<UserSettings>(a => a.UserSettings)
                .WithMany(g => g.Clips)
                .HasForeignKey(s => s.OwnerId)
                .HasPrincipalKey(k => k.OwnerId);

            builder.Entity<Following>()
                .HasOne<UserSettings>(a => a.YouFollow)
                .WithMany(g => g.YouFollowing)
                .HasForeignKey(a => a.YouFollowId)
                .HasPrincipalKey(g => g.OwnerId);

            builder.Entity<Following>()
                .HasOne<UserSettings>(a => a.TheyFollowing)
                .WithMany(g => g.TheyFollowing)
                .HasForeignKey(a => a.TheyFollowingId)
                .HasPrincipalKey(g => g.OwnerId);

            builder.Entity<Notifications>()
                .HasOne<UserSettings>(a => a.UserSettings)
                .WithOne(g => g.Notifications)
                .HasForeignKey<Notifications>(k => k.OwnerId)
                .HasPrincipalKey<UserSettings>(k => k.OwnerId);
        }
    }
}