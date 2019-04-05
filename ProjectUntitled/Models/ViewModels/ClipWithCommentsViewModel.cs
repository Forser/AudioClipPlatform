using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectUntitled.Models.ViewModels
{
    public class ClipWithCommentsViewModel
    {
        public ClipWithCommentsViewModel()
        {
            NewComment = new CommentViewModel();
            NewComment.ClipId = Id;
        }

        public int Id { get; set; }

        public string UploadedBy { get; set; }

        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime UploadAt { get; set; }

        public string ContentCreator { get; set; }

        public ContentPlatform ContentPlatform { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Don't forget to add a comment")]
        public CommentViewModel NewComment { get; set; }

        public string ReturnUrl { get; set; }
    }
}
