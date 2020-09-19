using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_ktr_msc.Entities.Identity;
using Project_ktr_msc.Entities.Profiles;

namespace Project_ktr_msc.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.OwnProfile)
                .WithOne(p => p.Owner)
                .HasForeignKey<UserProfile>(p => p.OwnerId);

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.Library)
                .WithOne(l => l.Owner)
                .HasForeignKey<Library>(l => l.OwnerId);

            builder.Entity<Library>()
                .HasMany(l => l.Profiles)
                .WithOne(p => p.Library);
        }
    }
}
