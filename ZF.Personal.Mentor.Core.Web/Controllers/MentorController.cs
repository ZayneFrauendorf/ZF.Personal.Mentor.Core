using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZF.Personal.Mentor.Core.Data.Models;
using ZF.Personal.Mentor.Core.Domain.Services;
using ZF.Personal.Mentor.Core.Web.ViewModels;

namespace ZF.Personal.Mentor.Core.Web.Controllers
{
    public class MentorController : Controller
    {
        private readonly IUserService _userService;

        public MentorController(IUserService userService)
        {
            this._userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var mentors = await this._userService.GetMentorsAsync();

            MentorListViewModel mentorListViewModel = new MentorListViewModel
            {
                Profiles = mentors.Select(x => x.Profile).ToList(),
                IsMentor = await this._userService.IsMentorAsync(User.Identity.Name)
            };
            return View(mentorListViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var mentor = await this._userService.GetUserByProfileIdAsync(id);

            var viewModel = new ViewMentorViewModel
            {
                Profile = mentor.Profile
            };
            return View(viewModel);
        }

        public async Task<IActionResult> BecomeMentor()
        {
            if (await this._userService.IsMentorAsync(User.Identity.Name))
            {
                return RedirectToAction(nameof(Index));
            }
            await this._userService.AddRoletoUserAsync(User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }
    }
}