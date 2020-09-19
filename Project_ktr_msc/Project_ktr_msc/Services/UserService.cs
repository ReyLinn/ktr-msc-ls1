using Microsoft.EntityFrameworkCore;
using Project_ktr_msc.Data;
using Project_ktr_msc.Entities.Identity;
using Project_ktr_msc.Entities.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_ktr_msc.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        
        public async Task<List<Profile>> GetUserProfilesById(string id)
        {
            return (await _context.Users.Include(u => u.Profiles).FirstOrDefaultAsync(u => u.Id == id))?.Profiles.ToList();
        }
        public async Task<bool> AddProfileToUser(string userId, Profile profileToAdd)
        {
            var user = await _context.Users.Include(u => u.Profiles).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return false;
            profileToAdd.Owner = user;
            _context.Profiles.Add(profileToAdd);
            user.Profiles.Add(profileToAdd);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateProfileOfUser(string userId, Profile profileUpdated)
        {
            var user = await _context.Users.Include(u => u.Profiles).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return false;
            var profileId = profileUpdated.Id;
            var profileToDelete = user.Profiles.FirstOrDefault(p => p.Id == profileId);
            user.Profiles.Remove(profileToDelete);
            _context.Profiles.Update(profileUpdated);
            user.Profiles.Add(profileUpdated);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteProfileOfUser(string userId, Profile profileToDelete)
        {
            var user = await _context.Users.Include(u => u.Profiles).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return false;
            if (user.Profiles.Contains(profileToDelete))
            {
                user.Profiles.Remove(profileToDelete);
                _context.Profiles.Remove(profileToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<Profile> GetProfileById(int id)
        {
            return await _context.Profiles.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
