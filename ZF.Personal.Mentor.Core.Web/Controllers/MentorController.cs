using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var mentors = await this._userService.GetUsersByRoleAsync("Mentor");

            MentorListViewModel mentorListViewModel = new MentorListViewModel
            {
                Profiles = mentors.Select(x => x.Profile).ToList()
            };
            return View(mentorListViewModel);
        }
    }
}