using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZF.Personal.Mentor.Core.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ZF.Personal.Mentor.Core.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        public async Task<IList<ApplicationUser>> GetAllUsersAsync()
        {
            return await this._context.Users.Include(x => x.Profile).Include(u => u.UserRoles).ThenInclude(ur => ur.Role).AsNoTracking().ToListAsync();
        }

        public async Task<ApplicationUser> GetUserAsync(string email)
        {
            return (await this.GetAllUsersAsync()).FirstOrDefault(x => x.Email == email);
        }

        public async Task<IList<ApplicationUser>> GetUsersByRoleAsync(string role)
        {
            return (await this.GetAllUsersAsync()).Where(x => x.UserRoles.Any(y => y.Role.Name == role)).ToList();
        }

        public async Task AddRoletoUserAsync(string email)
        {

            var user = this._context.Users.Where(x => x.Email == email).FirstOrDefault();
            await this._userManager.AddToRoleAsync(user, "MENTOR");
        }
    }
}
