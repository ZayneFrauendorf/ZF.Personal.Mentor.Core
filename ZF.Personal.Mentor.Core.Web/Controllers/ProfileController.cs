using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IHostingEnvironment _appEnvironment;
        public ProfileController(IUserService userService, IHostingEnvironment appEnvironment)
        {
            this._userService = userService;
            this._appEnvironment = appEnvironment;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var appUser = await this._userService.GetUserAsync(User.Identity.Name);

            var viewModel = new ProfileViewModel
            {
                FirstName = appUser.Profile.FirstName,
                LastName = appUser.Profile.LastName,
                Description = appUser.Profile.Description,
                DisplayName = appUser.Profile.DisplayName
            };

            ViewBag.ProfileImage = appUser.Profile.ProfileId.ToString() + ".jpg";

            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userService.GetUserAsync(User.Identity.Name);

            var viewModel = new ProfileViewModel
            {
                FirstName = user.Profile.FirstName,
                LastName = user.Profile.LastName,
                Description = user.Profile.Description,
                DisplayName = user.Profile.DisplayName
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(ProfileViewModel viewModel, IFormFile file = null)
        {
            var user = await _userService.GetUserAsync(User.Identity.Name);

            var model = new Profile
            {
                ProfileId = user.ProfileId,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Description = viewModel.Description,
                DisplayName = viewModel.DisplayName
            };

            await this._userService.UpdateProfileAsync(model);
            // return Content("file not selected");

            if (file != null)
            {
                if (file.Length != 0)
                {

                    string path_Root = _appEnvironment.WebRootPath;

                    string path_to_Images = path_Root + "\\UserFiles\\Images\\" + user.ProfileId + ".jpg";

                    using (var stream = new FileStream(path_to_Images, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

            return RedirectToAction(nameof(Edit));
        }
    }
}