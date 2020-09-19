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
        public Task<ApplicationUser> GetUserById(string id);
        public Task<List<Profile>> GetUserProfilesById(string id);
        public Task<bool> AddProfileToUser(string userId, Profile profileToAdd);
        public Task<bool> UpdateProfileOfUser(string userId, Profile profileUpdated);
        public Task<bool> DeleteProfileOfUser(string userId, Profile profileToDelete);
        public Task<Profile> GetProfileById(int id);
    }
}
