using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_ktr_msc.Entities.Profiles;

namespace Project_ktr_msc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        DbSet<Profile> Profiles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Profile>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.Profiles);
        }
    }
}
