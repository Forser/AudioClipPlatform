using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Project_Untitled.Models.ViewModels
{
    public class MessagesViewModel
    {
        public IList<Messages> Messages { get; set; }

        public Messages NewMessage { get; set; }

        public IdentityUser User { get; set; }
    }
}
