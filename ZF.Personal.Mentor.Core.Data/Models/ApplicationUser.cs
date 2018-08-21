using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZF.Personal.Mentor.Core.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
