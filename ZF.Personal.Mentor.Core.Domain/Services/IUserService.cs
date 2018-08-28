using System.Collections.Generic;
using System.Threading.Tasks;
using ZF.Personal.Mentor.Core.Data.Models;

namespace ZF.Personal.Mentor.Core.Domain.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserAsync(string email);
        Task<IList<ApplicationUser>> GetUsersByRoleAsync(string role);
        Task UpdateProfileAsync(Profile profile);
        Task AddRoletoUserAsync(string email);
        Task<ApplicationUser> GetUserByProfileIdAsync(int id);
        Task<IList<ApplicationUser>> GetMentorsAsync();
        Task<bool> IsMentorAsync(string email);

    }
}