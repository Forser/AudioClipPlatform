using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ProjectUntitled.Models.ViewModels
{
    public class NewProfileImageView
    {
        [Required(ErrorMessage = "Must select a file to upload", AllowEmptyStrings = false)]
        public IFormFile formFile { get; set; }

        public string FileName { get; set; }
    }
}
