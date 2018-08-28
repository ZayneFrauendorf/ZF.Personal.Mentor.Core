using System.Collections.Generic;
using System.Threading.Tasks;
using ZF.Personal.Mentor.Core.Data.Models;

namespace ZF.Personal.Mentor.Core.Data.Repositories
{
    public interface IMessageRepository
    {
        Task<IList<Message>> GetAllMessagesForUserAsync(string email);
        Task AddMessageAsync(Message message);
    }
}