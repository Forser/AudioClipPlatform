using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
            MessagesViewModel messagesViewModel = await GetMessages();

            return View(messagesViewModel);
        }

        private async Task<MessagesViewModel> GetMessages()
        {
            var _user = await userManager.GetUserAsync(HttpContext.User);
            var _messages = messageRepository.GetMessages(_user);
            var _newMessage = new Message { SenderId = _user.Id, SenderUserName = _user.UserName };

            var messageSource = new MessagesViewModel
            {
                Messages = Mapper.Map<IEnumerable<MessageViewModel>>(_messages),
                NewMessage = Mapper.Map<Message, NewMessageViewModel>(_newMessage)
            };

            var messagesViewModel = Mapper.Map<MessagesViewModel>(messageSource);
            return messagesViewModel;
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
        public async Task<IActionResult> SendMessage([FromForm]NewMessageViewModel newMessage)
        {
            if(ModelState.IsValid)
            {
                var _user = await userManager.FindByNameAsync(newMessage.RecipentUserName);
                
                if(_user != null)
                {
                    var _sender = await userManager.GetUserAsync(HttpContext.User);

                    newMessage.RecipentId = _user.Id;
                    newMessage.SenderId = _sender.Id;
                    newMessage.SenderUserName = _sender.UserName;

                    var _message = Mapper.Map<Message>(newMessage);
                    var Succeeded = await messageRepository.SendMessage(_message);

                    if (Succeeded)
                    {
                        TempData["message"] = "Your message has been sent!";
                        return Redirect("Index");
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
                var _sender = await userManager.GetUserAsync(HttpContext.User);
                var Succeeded = await messageRepository.ReplyMessage(replyMessage, _sender);

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