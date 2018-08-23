using System;
using System.Collections.Generic;
using System.Text;

namespace ZF.Personal.Mentor.Core.Data.Models
{
    public class Message
    {
        public int MessageId { get; set; }

        public ApplicationUser From { get; set; }
        public string FromId { get; set; }

        public ApplicationUser To { get; set; }
        public string ToId { get; set; }

        public DateTime SentAt { get; set; }
        public DateTime ReadAt { get; set; }

        public string Body { get; set; }
    }
}
