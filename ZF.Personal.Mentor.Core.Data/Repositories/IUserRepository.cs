using System.Collections.Generic;
using System.Threading.Tasks;
using ZF.Personal.Mentor.Core.Data.Models;

namespace ZF.Personal.Mentor.Core.Data.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserAsync(string email);
        Task<IList<ApplicationUser>> GetAllUsersAsync();
        Task<IList<ApplicationUser>> GetUsersByRoleAsync(string role);

        Task AddRoletoUserAsync(string email);
        Task<ApplicationUser> GetUserByProfileIdAsync(int id);
    }
}