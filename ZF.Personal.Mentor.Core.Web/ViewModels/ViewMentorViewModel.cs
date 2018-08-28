using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZF.Personal.Mentor.Core.Data.Models;

namespace ZF.Personal.Mentor.Core.Web.ViewModels
{
    public class ViewMentorViewModel
    {
        public Profile Profile { get; set; }
        public string ProfileImage => $"{this.Profile.ProfileId}.jpg";
    }
}
