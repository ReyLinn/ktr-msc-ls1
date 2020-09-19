using Project_ktr_msc.Entities.Identity;
using Project_ktr_msc.Entities.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_ktr_msc.Services
{
    public interface IUserService
    {
        public Task<ApplicationUser> GetUserById(string userId);
        public Task<UserProfile> GetUserProfileById(string userId);
        public Task<bool> AddProfileToUser(string userId, UserProfile profileToAdd);
        public Task<bool> UpdateProfileOfUser(string userId, UserProfile profileUpdated);
        public Task<Library> GetUserLibraryById(string userId);
        public Task<bool> AddProfileToUserLibrary(string userId, Profile profileToAdd);
        public Task<bool> UpdateProfileOfUserLibrary(string userId, Profile profileUpdated);
        public Task<bool> DeleteProfileOfUserLibrary(string userId, Profile profileToDelete);
        public Task<Profile> GetProfileById(int profileId);
        public Task<Library> CreateLibraryForUser(string userId);
    }
}
