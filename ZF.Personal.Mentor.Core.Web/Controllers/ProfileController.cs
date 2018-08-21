using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZF.Personal.Mentor.Core.Data.Models;
using ZF.Personal.Mentor.Core.Domain.Services;
using ZF.Personal.Mentor.Core.Web.ViewModels;

namespace ZF.Personal.Mentor.Core.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            this._userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            // Get current user id
            
            var appUser = await this._userService.GetUserAsync(User.Identity.Name);

            var viewModel = new ProfileViewModel
            {
                FirstName = appUser.Profile.FirstName,
                LastName = appUser.Profile.LastName,
                Description = appUser.Profile.Description,
                DisplayName = appUser.Profile.DisplayName
            };

           
            return View(viewModel);
        }
    }
}