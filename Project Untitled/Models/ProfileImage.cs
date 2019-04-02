using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectUntitled.Models
{
    public class ProfileImage
    {
        public int id { get; set; }

        public string ProfileImg { get; set; }

        public string OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public UserSettings UserSettings { get; set; }
    }
}
