namespace ProjectUntitled.Models
{
    public class Liked
    {
        public int Id { get; set; }

        public string LikeByUserId { get; set; }

        public int ClipId { get; set; }
        public Clips Clips { get; set; }
    }
}