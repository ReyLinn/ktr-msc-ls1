using Project_ktr_msc.Entities.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_ktr_msc.Models
{
    public class ProfileViewModel
    {
        public bool IsProfileCreated { get; set; }
        public UserProfile Profile { get; set; }
    }
}
