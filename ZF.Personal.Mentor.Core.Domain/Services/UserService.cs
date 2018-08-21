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

        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
            this._userRepository = userRepository;
        }

        public async Task<ApplicationUser> GetUserAsync(string email)
        {
            return await this._userRepository.GetUserAsync(email);
        }

        public async Task<IList<ApplicationUser>> GetUsersByRoleAsync(string role)
        {
            return await this._userRepository.GetUsersByRoleAsync(role);
        }
    }
}
