using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZF.Personal.Mentor.Core.Domain.Services;
using ZF.Personal.Mentor.Core.Web.ViewModels;

namespace ZF.Personal.Mentor.Core.Web.Controllers
{
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
            //var messages = await this._messageService.GetAllMessagesForUserAsync(User.Identity.Name);
            MessageListViewModel viewModel = new MessageListViewModel
            {
                Messages = await this._messageService.GetAllMessagesForUserAsync(User.Identity.Name)
                //MessageBodies = messages.Select(x => x.Body);
            };
            return View(viewModel);
        }
       
    }
}