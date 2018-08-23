using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZF.Personal.Mentor.Core.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ZF.Personal.Mentor.Core.Data.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;
        public ProfileRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task UpdateProfileAsync(Profile profile)
        {
            this._context.Entry(profile).State = EntityState.Modified;
        }

        public async Task SaveAsync()
        {
            await this._context.SaveChangesAsync();
        }

    }
}
