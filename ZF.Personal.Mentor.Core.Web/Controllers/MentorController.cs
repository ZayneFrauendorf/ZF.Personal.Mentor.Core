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
            var mentors = await this._userService.GetUsersByRoleAsync("MENTOR");

            MentorListViewModel mentorListViewModel = new MentorListViewModel
            {
                Profiles = mentors.Select(x => x.Profile).ToList()
            };
            return View(mentorListViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ViewProfile(int id)
        {
            var mentors = await this._userService.GetUsersByRoleAsync("MENTOR");

            var mentor = mentors.Select(x => x.Profile).Where(x => x.ProfileId == id).FirstOrDefault();

            ViewBag.ProfileImage = mentor.ProfileId + ".jpg";

            var profileViewModel = new ProfileViewModel
            {
                DisplayName = mentor.DisplayName,
                Description = mentor.Description,
                FirstName = mentor.FirstName,
                LastName = mentor.LastName
            };
            return View(profileViewModel);
        }

        public async Task<IActionResult> BecomeMentor()
        {
            if (User.IsInRole("MENTOR"))
            {
                return RedirectToAction(nameof(Index));
            }
            //var user = await this._userService.GetUserAsync(User.Identity.Name);
            await this._userService.AddRoletoUserAsync(User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }
    }
}