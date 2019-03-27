using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Untitled.Models;
using Project_Untitled.Models.ViewModels;

namespace Project_Untitled.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IMessageRepository messageRepository;
        private readonly UserManager<IdentityUser> userManager;

        public MessagesController(IMessageRepository messageRepo, UserManager<IdentityUser> usrMgr)
        {
            messageRepository = messageRepo;
            userManager = usrMgr;
        }

        public async Task<IActionResult> Index()
        {
            var _user = await userManager.GetUserAsync(HttpContext.User);
            var _messages = messageRepository.GetMessages(_user);

            return View(new MessagesViewModel() { Messages = _messages, User = _user, NewMessage = new Messages() });
        }

        public async Task<IActionResult> GetMessage(int messageId)
        {
            var _user = await userManager.GetUserAsync(HttpContext.User);
            var _message = messageRepository.GetMessage(messageId, _user.Id);

            return View(_message);
        }

        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            var _user = await userManager.GetUserAsync(HttpContext.User);
            messageRepository.ChangeMessageStatus(messageId, _user.Id, MessageStatus.Deleted);

            TempData["message"] = "Message deleted!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(MessagesViewModel messagesViewModel)
        {
            if(ModelState.IsValid)
            {
                var Succeeded = await messageRepository.SendMessage(messagesViewModel);

                if (Succeeded)
                {
                    TempData["message"] = "Your message has been sent!";
                    return Redirect("Index");
                }
            }
            var _user = await userManager.GetUserAsync(HttpContext.User);
            var _messages = messageRepository.GetMessages(_user);

            messagesViewModel.Messages = _messages;

            ModelState.AddModelError("", "Error: Could not send your message!");
            return View("Index", messagesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReplyMessage(GetMessageViewModel replyMessage)
        {
            if(ModelState.IsValid)
            {
                var Succeeded = await messageRepository.ReplyMessage(replyMessage);

                if (Succeeded)
                {
                    TempData["message"] = "Your message has been sent!";
                    return Redirect("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Could not send your message!");
                    return View("GetMessage", replyMessage);
                }
            }

            ModelState.AddModelError("", "Error: Could not send your message!");
            return View("GetMessage", replyMessage);
        }
    }
}