using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
}