using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Untitled.Models;
using Project_Untitled.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Untitled.Controllers
{
    [Authorize]
    public class ClipsController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IClipsRepository clipsRepository;

        public ClipsController(UserManager<IdentityUser> usrManager, IClipsRepository clipsRepo)
        {
            userManager = usrManager;
            clipsRepository = clipsRepo;
        }

        [AllowAnonymous]
        [HttpGet("/Clip/{id:int}")]
        public IActionResult Index(int Id)
        {
            var clipWithComments = clipsRepository.GetClipWithComments(Id);

            clipWithComments.Comments = clipWithComments.Comments.OrderByDescending(s => s.PostedAt).ToList<Comment>();

            var clipWithCommentsViewModel = Mapper.Map<ClipWithCommentsViewModel>(clipWithComments);

            if(clipWithCommentsViewModel == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(clipWithCommentsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendComment([FromForm]ClipWithCommentsViewModel commentViewModel)
        {
            var Succeeded = false;
            
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);

                if(user != null)
                {
                    commentViewModel.NewComment.UserName = user.UserName;
                    commentViewModel.NewComment.ClipId = commentViewModel.Id;
                    var newComment = Mapper.Map<Comment>(commentViewModel.NewComment);

                    Succeeded = await clipsRepository.SendComment(newComment);

                    if (Succeeded)
                    {
                        TempData["message"] = "Your comment has been sent!";
                        return Redirect(commentViewModel?.ReturnUrl ?? "/");
                    }
                }
            }

            ModelState.AddModelError("", "Error: Could not send your comment!");
            return View("Index", commentViewModel);
        }
    }
}