using System.Collections.Generic;
using System.Threading.Tasks;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Domain.Data.Repositories
{
    public interface IProfileRepository : IRepository<Profile>
    {
        Task<Profile> FindByUsername(string username);
        Task<ICollection<Profile>> GetFollowers(Profile profile);
        Task<ICollection<Profile>> GetFollowing(Profile profile);
        Task<bool> AddFollowing(Profile followingProfile, Profile followerProfile);
        Task<bool> RemoveFollowing(Profile followingProfile, Profile followerProfile);
    }
}