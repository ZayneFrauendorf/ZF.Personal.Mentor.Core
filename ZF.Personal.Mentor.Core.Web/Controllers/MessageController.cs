using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ZF.Personal.Mentor.Core.Data.Models;
using ZF.Personal.Mentor.Core.Domain.Services;
using ZF.Personal.Mentor.Core.Web.ViewModels;
using System.Linq;
using ReflectionIT.Mvc.Paging;

namespace ZF.Personal.Mentor.Core.Web.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private IMessageService _messageService;
        private IUserService _userService;
        public MessageController(IMessageService messageService, IUserService userService)
        {
            this._messageService = messageService;
            this._userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            MessageListViewModel viewModel = new MessageListViewModel
            {
                Messages = await this._messageService.GetAllMessagesForUserAsync(User.Identity.Name)
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> SendMessage(int id)
        {
            var intendedReceiver = await this._userService.GetUserByProfileIdAsync(id);

            var loggedInUser = await this._userService.GetUserAsync(User.Identity.Name);

            SendMessageViewModel viewModel = new SendMessageViewModel
            {
                Message = new Message
                {
                    Body = string.Empty,
                    To = intendedReceiver,
                    ToId = intendedReceiver.Id,
                    From = loggedInUser,
                    FromId = loggedInUser.Id
                }
            };

            return View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessageViewModel viewModel, int id)
        {
            var intendedReceiver = await this._userService.GetUserByProfileIdAsync(id);
            var loggedInUser = await this._userService.GetUserAsync(User.Identity.Name);

            viewModel.Message.ToId = intendedReceiver.Id;
            viewModel.Message.FromId = loggedInUser.Id;
            viewModel.Message.SentAt = DateTime.UtcNow;

            await this._messageService.SendMessage(viewModel.Message);
            return RedirectToAction("Index", "Mentor");
        }

    }
}