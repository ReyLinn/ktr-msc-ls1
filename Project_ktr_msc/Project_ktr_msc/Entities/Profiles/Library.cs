using Project_ktr_msc.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_ktr_msc.Entities.Profiles
{
    public class Library
    {
        [Key]
        public int Id { get; set; }

        public ICollection<Profile> Profiles { get; set; }

        public string OwnerId { get; set; }
        [Required]
        public ApplicationUser Owner { get; set; }
    }
}
