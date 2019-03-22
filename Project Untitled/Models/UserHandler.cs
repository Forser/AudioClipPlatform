using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Project_Untitled.Models
{
    public class UserHandler : IdentityUser
    {
        // User Data
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
        [PersonalData]
        public string Gender { get; set; }
        [PersonalData]
        public string Location { get; set; }
        [PersonalData]
        public string Biography { get; set; }
        [PersonalData]
        public string ProfileImage { get; set; }
        public string HeaderImage { get; set; }

        // Social Media
        [PersonalData]
        public string Twitter { get; set; }
        [PersonalData]
        public string Facebook { get; set; }
        [PersonalData]
        public string Instagram { get; set; }
        [PersonalData]
        public string Tumblr { get; set; }
        [PersonalData]
        public string Reddit { get; set; }

        // Streaming Platform
        [PersonalData]
        public string Twitch { get; set; }
        [PersonalData]
        public string Mixer { get; set; }
        [PersonalData]
        public string YouTube { get; set; }
        [PersonalData]
        public string FacebookLive { get; set; }
        [PersonalData]
        public string Periscope { get; set; }
        [PersonalData]
        public string LiveStream { get; set; }

        // Privacy
        public bool AllowMessages { get; set; }

        // Email / Device notifications
        public ICollection<Notifications> Notifications { get; set; }

        // Blacklisted Users
        // public List<IdentityUser> BlacklistedUsers { get; set; }
    }
}