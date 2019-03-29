using System.ComponentModel.DataAnnotations;

namespace Project_Untitled.Models
{
    public class UserHandler
    {
        [Key]
        public int? Id { get; set; }

        // Identity UserID
        public string IdentityId { get; set; }

        // User Settings
        public UserSettings UserSettings { get; set; }

        // Email / Device notifications
        public Notifications Notifications { get; set; }

        // Profile
        public UserProfile UserProfile { get; set; }

        // Blacklisted Users
        // public List<IdentityUser> BlacklistedUsers { get; set; }

    }
}