using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ProjectUntitled.Models.ViewModels
{
    public class FileUploadView
    {
        [Required(ErrorMessage = "Must select a file to upload")]
        public IFormFile formFile { get; set; }

        public string FileName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter a title for the AudioClip", AllowEmptyStrings = false)]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter the name of the Content Creator it comes from!", AllowEmptyStrings = false)]
        public string ContentCreator { get; set; }

        [Required(ErrorMessage = "Select what Content Platform the Content Creator is on!")]
        public ContentPlatform ContentPlatform { get; set; }

    }
}
