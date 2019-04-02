using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectUntitled.Models
{
    public class Notifications
    {
        [Key]
        public int Id { get; set; }

        // UserID
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public UserSettings UserSettings {get; set;}

        // Desktop Notification
        public bool DesktopNotification { get; set; }

        // User Notifications
        public bool NewFollowerEmail { get; set; }
        public bool NewFollowerDevice { get; set; }

        public bool RepostOfPostEmail { get; set; }
        public bool RepostOfPostDevice { get; set; }

        public bool NewPostByFollowedUserEmail { get; set; }
        public bool NewPostByFollowedUserDevice { get; set; }

        public bool LikeOnYourPostEmail { get; set; }
        public bool LikeOnYourPostDevice { get; set; }

        public bool CommentOnYourPostEmail { get; set; }
        public bool CommentOnYourPostDevice { get; set; }

        public bool SuggestedContentEmail { get; set; }
        public bool SuggestedContentDevice { get; set; }

        public bool NewMessageEmail { get; set; }
        public bool NewMessageDevice { get; set; }

        // Updates from Developers
        public bool NewFeatureEmail { get; set; }
        public bool NewFeatureDevice { get; set; }

        public bool SurveyAndFeedbackEmail { get; set; }
        public bool SurveyAndFeedbackDevice { get; set; }

        public bool TipsAndOffersEmail { get; set; }
        public bool TipsAndOffersDevice { get; set; }

        public bool NewsLetterEmail { get; set; }
        public bool NewsLetterDevice { get; set; }
    }
}