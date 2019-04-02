using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectUntitled.Models.ViewModels
{
    public class UserSettingsViewModel
    {
        [RegularExpression(@"^[0-9""'\s-]*$"), Required]
        public int Id { get; set; }

        [DataType(DataType.Text), StringLength(maximumLength: 50, ErrorMessage = "Can't be longer then 50 characters")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Gender { get; set; }

        public bool AllowMessages { get; set; }

        [DataType(DataType.Date), Display(Name = "Date of Birth"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Text)]
        public string Location { get; set; }

        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }

        [DataType(DataType.Text)]
        public string Twitter { get; set; }

        [DataType(DataType.Text)]
        public string Facebook { get; set; }

        [DataType(DataType.Text)]
        public string Instagram { get; set; }

        [DataType(DataType.Text)]
        public string Tumblr { get; set; }

        [DataType(DataType.Text)]
        public string Reddit { get; set; }

        [DataType(DataType.Text)]
        public string Spotify { get; set; }

        [DataType(DataType.Text)]
        public string Wordpress { get; set; }

        [DataType(DataType.Text)]
        public string Twitch { get; set; }

        [DataType(DataType.Text)]
        public string Youtube { get; set; }

        [DataType(DataType.Text)]
        public string Mixer { get; set; }
    }
}