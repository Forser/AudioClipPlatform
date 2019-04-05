using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectUntitled.Models
{
    public class Clips
    {
        [Key]
        public int Id { get; set; }

        public string UploadedBy { get; set; }

        public string FileName { get; set; }

        [Display(Name = "Title for clip")]
        public string Title { get; set; }

        public DateTime UploadAt { get; set; } = DateTime.Now;

        public FileStatus FileStatus { get; set; } = FileStatus.Unlisted;

        [Display(Name = "Content Creator's Name")]
        public string ContentCreator { get; set; }

        [Display(Name = "Content Creator's Platform")]
        public ContentPlatform ContentPlatform { get; set; } = ContentPlatform.Twitch;

        public IList<Liked> Likes { get; set; }

        public IList<Comment> Comments { get; set; }

        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public UserSettings UserSettings { get; set; }
    }

    public enum FileStatus
    {
        Unlisted,
        Flagged,
        Deleted,
        Listed
    }

    public enum ContentPlatform
    {
        Twitch,
        Youtube,
        Mixer
    }
}