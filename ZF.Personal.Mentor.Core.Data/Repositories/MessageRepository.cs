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
            return await this._context.Messages.Include(x => x.From.Profile).Where(x => x.To == user).OrderBy(x => x.SentAt).ToListAsync();
        }

        public async Task AddMessageAsync(Message message)
        {
            this._context.Entry(message).State = EntityState.Added;
            await this._context.SaveChangesAsync();
        }
    }
}
