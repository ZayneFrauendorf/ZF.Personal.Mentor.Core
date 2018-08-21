using System.Collections.Generic;
using System.Threading.Tasks;
using ZF.Personal.Mentor.Core.Data.Models;

namespace ZF.Personal.Mentor.Core.Domain.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserAsync(string email);
        Task<IList<ApplicationUser>> GetUsersByRoleAsync(string role);
    }
}