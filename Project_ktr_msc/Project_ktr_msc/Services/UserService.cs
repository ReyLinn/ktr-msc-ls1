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

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await _context.Users
                .Include(u => u.OwnProfile)
                .Include(u => u.Library)
                    .ThenInclude(l => l.Profiles)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<bool> AddProfileToUser(string userId, UserProfile profileToAdd)
        {
            var user = await GetUserById(userId);
            profileToAdd.Owner = user;
            _context.UserProfiles.Add(profileToAdd);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserProfile> GetUserProfileById(string id)
        {
            return (await _context.Users
                .Include(u => u.OwnProfile)
                .FirstOrDefaultAsync(u => u.Id == id))?.OwnProfile;
        }

        public async Task<bool> UpdateProfileOfUser(string userId, UserProfile profileUpdated)
        {
            var user = await GetUserById(userId);
            if (user == null) return false;
            var profileToDelete = user.OwnProfile;
            user.OwnProfile = null;
            _context.UserProfiles.Update(profileUpdated);
            user.OwnProfile = profileUpdated;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Library> GetUserLibraryById(string id)
        {
            return (await _context.Users
                .Include(u => u.Library)
                    .ThenInclude(l => l.Profiles)
                .FirstOrDefaultAsync(u => u.Id == id))?.Library;
        }

        public async Task<bool> AddProfileToUserLibrary(string userId, Profile profileToAdd)
        {
            var user = await GetUserById(userId);
            if (user == null) return false;
            _context.Profiles.Add(profileToAdd);
            user.Library.Profiles.Add(profileToAdd);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProfileOfUserLibrary(string userId, Profile profileUpdated)
        {
            var user = await GetUserById(userId);
            if (user == null) return false;
            var profileToDelete = user.Library.Profiles.FirstOrDefault(p => p.Id == profileUpdated.Id);
            if (profileToDelete == null) return false;
            user.Library.Profiles.Remove(profileToDelete);
            _context.Profiles.Update(profileUpdated);
            user.Library.Profiles.Add(profileToDelete);
            user.Library.Profiles.OrderBy(p => p.Id);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProfileOfUserLibrary(string userId, Profile profileToDelete)
        {
            var user = await GetUserById(userId);
            if (user == null) return false;
            if (user.Library.Profiles.Contains(profileToDelete))
            {
                user.Library.Profiles.Remove(profileToDelete);
                _context.Profiles.Remove(profileToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Profile> GetProfileById(int profileId)
        {
            return await _context.Profiles.FirstOrDefaultAsync(p => p.Id == profileId);
        }

        public async Task<Library> CreateLibraryForUser(string userId)
        {
            var user = await GetUserById(userId);
            if (user == null) return null;
            Library newLibrary = new Library()
            {
                Owner = user,
                OwnerId = user.Id,
                Profiles = new List<Profile>()
            };
            user.Library = newLibrary;
            _context.Libraries.Add(newLibrary);
            await _context.SaveChangesAsync();
            return newLibrary;
        }
    }
}
