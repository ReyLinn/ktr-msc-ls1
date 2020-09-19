using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Project_ktr_msc.Entities.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_ktr_msc.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public UserProfile OwnProfile { get; set; }
        public Library Library { get; set; }
    }
}
