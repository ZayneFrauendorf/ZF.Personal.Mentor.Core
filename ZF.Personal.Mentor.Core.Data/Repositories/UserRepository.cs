using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZF.Personal.Mentor.Core.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ZF.Personal.Mentor.Core.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IList<ApplicationUser>> GetAllUsersAsync()
        {
            return await this._context.Users.Include(x => x.Profile).Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ToListAsync();
        }

        public async Task<ApplicationUser> GetUserAsync(string email)
        {
            return (await this.GetAllUsersAsync()).FirstOrDefault(x => x.Email == email);
        }

        public async Task<IList<ApplicationUser>> GetUsersByRoleAsync(string role)
        {
            return (await this.GetAllUsersAsync()).Where(x => x.UserRoles.Any(y => y.Role.Name == role)).ToList();
        }
    }
}
