using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Project_Untitled.Models
{
    public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }

        public DbSet<UserHandler> UserHandler { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
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

            builder.Entity<UserHandler>()
                .ToTable("Users")
                .HasOne(a => a.Notifications)
                .WithOne()
                .HasForeignKey<Notifications>(b => b.UserId)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

            builder.Entity<Message>()
                .Property(e => e.SenderStatus)
                .HasConversion(messageConverter);

            builder.Entity<Message>()
                .Property(e => e.RecipentStatus)
                .HasConversion(messageConverter);

            builder.Entity<Clips>()
                .Property(e => e.FileStatus)
                .HasConversion(fileConverter);

            builder.Entity<UserHandler>()
                .HasOne<UserProfile>(a => a.UserProfile)
                .WithOne(ad => ad.User)
                .HasForeignKey<UserProfile>(ad => ad.UserId);

            builder.Entity<UserHandler>()
                .HasOne<UserSettings>(a => a.UserSettings)
                .WithOne(ad => ad.UserHandler)
                .HasForeignKey<UserSettings>(ad => ad.UserId);

            builder.Entity<Following>()
                .HasOne<UserProfile>(s => s.UserProfile)
                .WithMany(g => g.Following)
                .HasForeignKey(s => s.ProfileId);

            builder.Entity<Clips>()
                .HasOne<UserProfile>(s => s.UserProfile)
                .WithMany(g => g.Clips)
                .HasForeignKey(s => s.ProfileId);

            builder.Entity<Liked>()
                .HasOne<Clips>(s => s.Clips)
                .WithMany(g => g.Likes)
                .HasForeignKey(s => s.ClipId);
        }
    }
}