using Project_ktr_msc.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Project_ktr_msc.Entities.Profiles
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string EmailAdress  { get; set; }

        [Required]
        public string TelephoneNumber { get; set; }

        [Required]
        public ApplicationUser Owner { get; set; }
    }
}
