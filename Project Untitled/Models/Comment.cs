using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectUntitled.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public string UserName { get; set; }

        public DateTime PostedAt { get; set; }

        public int ClipId { get; set; }

        [ForeignKey("ClipId")]
        public Clips Clips { get; set; }

    }
}