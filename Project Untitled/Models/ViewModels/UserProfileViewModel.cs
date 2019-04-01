using System;

namespace Project_Untitled.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public string UserName { get; set; }
        public int? NumberOfFollowers { get; set; } = 0;
        public int? NumOfMembersYouFollow { get; set; } = 0;
        public int? NumberOfPublishedClips { get; set; } = 0;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Biography { get; set; }

        // Social Media
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Tumblr { get; set; }
        public string Reddit { get; set; }
        public string Spotify { get; set; }
        public string Wordpress { get; set; }

        // Streaming Platform
        public string Twitch { get; set; }
        public string Mixer { get; set; }
        public string Youtube { get; set; }

    }
}