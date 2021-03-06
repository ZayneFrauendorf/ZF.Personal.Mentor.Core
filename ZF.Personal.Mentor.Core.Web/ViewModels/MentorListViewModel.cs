﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZF.Personal.Mentor.Core.Data.Models;

namespace ZF.Personal.Mentor.Core.Web.ViewModels
{
    public class MentorListViewModel
    {
        public IList<Profile> Profiles { get; set; }
        public bool IsMentor { get; set; }
    }
}
