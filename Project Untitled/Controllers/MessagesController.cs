using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectUntitled.Models;
using ProjectUntitled.Models.ViewModels;

namespace ProjectUntitled.Controllers
{
    [Authorize]
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
            MessagesViewModel messagesViewModel = await GetMessages();

            return View(messagesViewModel);
        }

        private async Task<MessagesViewModel> GetMessages()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var messages = messageRepository.GetMessages(user);
            var newMessage = new Message { SenderId = user.Id, SenderUserName = user.UserName };

            var messageSource = new MessagesViewModel
            {
                Messages = Mapper.Map<IEnumerable<MessageViewModel>>(messages),
                NewMessage = Mapper.Map<Message, NewMessageViewModel>(newMessage)
            };

            var messagesViewModel = Mapper.Map<MessagesViewModel>(messageSource);
            return messagesViewModel;
        }

        public async Task<IActionResult> GetMessage(int messageId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var message = messageRepository.GetMessage(messageId, user.Id);

            return View(message);
        }

        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            messageRepository.ChangeMessageStatus(messageId, user.Id, MessageStatus.Deleted);

            TempData["message"] = "Message deleted!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage([FromForm]NewMessageViewModel newMessage)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(newMessage.RecipentUserName);

                if (user != null)
                {
                    var allowsMessages = messageRepository.AllowMessages(user.Id);

                    if (allowsMessages)
                    {
                        var sender = await userManager.GetUserAsync(HttpContext.User);

                        newMessage.RecipentId = user.Id;
                        newMessage.SenderId = sender.Id;
                        newMessage.SenderUserName = sender.UserName;

                        var message = Mapper.Map<Message>(newMessage);
                        var Succeeded = await messageRepository.SendMessage(message);

                        if (Succeeded)
                        {
                            TempData["message"] = "Your message has been sent!";
                            return Redirect("Index");
                        }
                    }
                }
            }

            MessagesViewModel messagesViewModel = await GetMessages();
            messagesViewModel.NewMessage = newMessage;

            ModelState.AddModelError("", "Error: Could not send your message!");
            return View("Index", messagesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReplyMessage([FromForm]GetMessageViewModel replyMessage)
        {
            if(ModelState.IsValid)
            {
                var sender = await userManager.GetUserAsync(HttpContext.User);
                var Succeeded = await messageRepository.ReplyMessage(replyMessage, sender);

                if (Succeeded)
                {
                    TempData["message"] = "Your message has been sent!";
                    return Redirect("Index");
                }
            }

            ModelState.AddModelError("", "Error: Could not send your message!");
            return View("GetMessage", replyMessage);
        }
    }
}