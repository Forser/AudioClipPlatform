namespace Project_Untitled.Models
{
    public class Following
    {
        public int Id { get; set; }
        public string FollowerId { get; set; }
        public string FollowingId { get; set; }

        public int ProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}