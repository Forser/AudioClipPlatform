using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Project_Untitled.Models.ViewModels
{
    public class UserViewModel
    {
        [BindRequired]
        public UserHandler User { get; set; }
    }
}