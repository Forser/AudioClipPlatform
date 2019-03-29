using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Untitled.Models
{
    public class Clips
    {
        [Key]
        public int Id { get; set; }

        public string FileName { get; set; }

        public DateTime UploadAt { get; set; } = DateTime.Now;

        public FileStatus FileStatus { get; set; }

        public IList<Liked> Likes { get; set; }

        public int ProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }

    public enum FileStatus
    {
        Unlisted,
        Flagged,
        Deleted,
        Listed
    }
}