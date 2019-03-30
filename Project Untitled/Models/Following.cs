using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Untitled.Models
{
    public class Following
    {
        public int Id { get; set; }

        public string YouFollowId { get; set; }
        [ForeignKey("YouFollowId")]
        public virtual UserSettings YouFollow { get; set; }

        public string TheyFollowingId { get; set; }
        [ForeignKey("TheyFollowingId")]
        public virtual UserSettings TheyFollowing { get; set; }
    }
}