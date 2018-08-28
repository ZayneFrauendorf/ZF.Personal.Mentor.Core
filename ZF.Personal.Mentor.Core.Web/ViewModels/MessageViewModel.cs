using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZF.Personal.Mentor.Core.Data.Models;

namespace ZF.Personal.Mentor.Core.Web.ViewModels
{
    public class MessageViewModel
    {

        public ApplicationUser From { get; set; }
        public string FromId { get; set; }

        public ApplicationUser To { get; set; }
        public string ToId { get; set; }

        public DateTime SentAt { get; set; }
        public DateTime ReadAt { get; set; }

        public string Body { get; set; }
    }
}
