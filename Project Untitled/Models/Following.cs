using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Untitled.Models
{
    public class Following
    {
        public int Id { get; set; }
        public string FollowerId { get; set; }

        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public UserSettings UserSettings { get; set; }
    }
}