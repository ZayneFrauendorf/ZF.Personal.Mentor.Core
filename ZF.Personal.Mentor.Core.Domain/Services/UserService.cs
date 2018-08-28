using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZF.Personal.Mentor.Core.Data.Models;
using ZF.Personal.Mentor.Core.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ZF.Personal.Mentor.Core.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private const string MENTOR_ROLE = "MENTOR";

        public UserService(IUserRepository userRepository, IProfileRepository profileRepository, UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
            this._userRepository = userRepository;
            this._profileRepository = profileRepository;
        }

        public async Task<ApplicationUser> GetUserAsync(string email)
        {
            return await this._userRepository.GetUserAsync(email);
        }

        public async Task<ApplicationUser> GetUserByProfileIdAsync(int id)
        {
            return await this._userRepository.GetUserByProfileIdAsync(id);
        }

        public async Task<IList<ApplicationUser>> GetUsersByRoleAsync(string role)
        {
            return await this._userRepository.GetUsersByRoleAsync(role);
        }

        public async Task UpdateProfileAsync(Profile profile)
        {
            await this._profileRepository.UpdateProfileAsync(profile);
            await this._profileRepository.SaveAsync();
        }

        public async Task<IList<ApplicationUser>> GetMentorsAsync()
        {
            return await this.GetUsersByRoleAsync(MENTOR_ROLE);
        }

        public async Task AddRoletoUserAsync(string email)
        {
            await this._userRepository.AddRoletoUserAsync(email);
        }

        public async Task<bool> IsMentorAsync(string email)
        {
            var getUser = await this.GetUserAsync(email);
            if (getUser == null)
            {
                return false;
            }
            return await this._userManager.IsInRoleAsync(getUser, MENTOR_ROLE);
        }
    }
}
