using System;
using System.Collections.Generic;

namespace Project_Untitled.Models.ViewModels
{
    public class ClipWithCommentsViewModel
    {
        public ClipWithCommentsViewModel()
        {
            NewComment = new CommentViewModel();
            NewComment.ClipId = Id;
        }

        public int Id { get; set; }

        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime UploadAt { get; set; }

        public string ContentCreator { get; set; }

        public string ContentPlatform { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public CommentViewModel NewComment { get; set; }

        public string ReturnUrl { get; set; }
    }
}
