using Microsoft.AspNetCore.Mvc;
using ProjectUntitled.Models;
using ProjectUntitled.Models.ViewModels;
using System.Linq;

namespace ProjectUntitled.ViewComponents
{
    public class FollowViewComponent : ViewComponent
    {
        private readonly AppIdentityDbContext context;

        public FollowViewComponent(AppIdentityDbContext ctx) { context = ctx; }

        public IViewComponentResult Invoke(string profileName)
        {
            FollowViewModel followViewModel = new FollowViewModel();

            followViewModel.FollowState = "default";

            var userIdentity = context.Users.Where(a => a.UserName == profileName).First();
            var userLoggedIn = context.Users.Where(a => a.UserName == User.Identity.Name).First();

            if (userIdentity.UserName != User.Identity.Name)
            {
                followViewModel.FollowState = (context.Followings.Where(t => t.YouFollowId == userLoggedIn.Id).Count() > 0) ? "Unfollow" : "Follow";
                followViewModel.MemberName = userIdentity.UserName;
            }

            return View("Default", followViewModel);
        }
    }
}