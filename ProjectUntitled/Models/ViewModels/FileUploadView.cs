using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ProjectUntitled.Models.ViewModels
{
    public class FileUploadView
    {
        public IFormFile formFile { get; set; }

        public string FileName { get; set; }

        [DataType(DataType.Text)]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        public string ContentCreator { get; set; }

        public ContentPlatform ContentPlatform { get; set; }

    }
}
