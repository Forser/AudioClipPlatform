using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectUntitled.Models
{
    public class UserSettings
    {
        public int Id { get; set; }

        // User Data
        [PersonalData]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [PersonalData]
        public DateTime DateOfBirth { get; set; }
        [PersonalData]
        public string Gender { get; set; }
        [PersonalData]
        public string Location { get; set; }
        [PersonalData]
        public string Biography { get; set; }

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
        [PersonalData]
        public string Wordpress { get; set; }
        [PersonalData]
        public string Spotify { get; set; }

        // Streaming Platform
        [PersonalData]
        public string Twitch { get; set; }
        [PersonalData]
        public string Mixer { get; set; }
        [PersonalData]
        public string Youtube { get; set; }

        // Privacy
        public bool AllowMessages { get; set; }

        // User Identity
        public string OwnerId { get; set; }

        // Email / Device notifications
        public Notifications Notifications { get; set; }

        // Followers / Following
        public virtual IList<Following> YouFollowing { get; set; }

        public virtual IList<Following> TheyFollowing { get; set; }

        // Users Clips
        public IList<Clips> Clips { get; set; }

        // Users Images
        public virtual ProfileImage ProfileImages { get; set; }
    }
}