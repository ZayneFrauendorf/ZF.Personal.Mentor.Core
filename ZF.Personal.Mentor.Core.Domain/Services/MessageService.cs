using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZF.Personal.Mentor.Core.Data.Models;
using ZF.Personal.Mentor.Core.Data.Repositories;

namespace ZF.Personal.Mentor.Core.Domain.Services
{
    public class MessageService : IMessageService
    {
        private IMessageRepository _messageRepository;
        public MessageService(IMessageRepository messageRepository)
        {
            this._messageRepository = messageRepository;
        }
        public async Task<IList<Message>> GetAllMessagesForUserAsync(string email)
        {
            return await this._messageRepository.GetAllMessagesForUserAsync(email);
        }
        public async Task SendMessage(Message message)
        {
            await this._messageRepository.AddMessageAsync(message);
        }
    }
}
