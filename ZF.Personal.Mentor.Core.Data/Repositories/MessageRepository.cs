using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZF.Personal.Mentor.Core.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ZF.Personal.Mentor.Core.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        public MessageRepository(ApplicationDbContext context, IUserRepository userRepository)
        {
            this._context = context;
            this._userRepository = userRepository;
        }

        public async Task<IList<Message>> GetAllMessagesForUserAsync(string email)
        {
            var user = await this._userRepository.GetUserAsync(email);
            return await this._context.Messages.Where(x => x.To == user).Include(x => x.From.Profile).ToListAsync();
        }
    }
}
