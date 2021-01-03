using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;
using Mublog.Server.Infrastructure.Identity;
using Mublog.Server.Infrastructure.Services.Interfaces;

namespace Mublog.Server.Infrastructure.Services
{
    public class JwtSubExtractorService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProfileRepository _profileRepo;
        private readonly IAccountManager _accountManager;

        public JwtSubExtractorService
        (
            IHttpContextAccessor httpContextAccessor,
            IProfileRepository profileRepo,
            IAccountManager accountManager
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _profileRepo = profileRepo;
            _accountManager = accountManager;
        }

        public CurrentUser Get()
        {
            var token = _httpContextAccessor.HttpContext?.User;

            var user = new CurrentUser();

            if (token == null || !token.HasClaim(c =>
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")) return null;
            {
                user.Username = token.Claims.FirstOrDefault(c =>
                    c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                user.AccountId = int.Parse(token.Claims.FirstOrDefault(c => c.Type == "accountId")?.Value ?? "0");
                user.ProfileId = int.Parse(token.Claims.FirstOrDefault(c => c.Type == "profileId")?.Value ?? "0");
            }

            return user;
        }

        public async Task<Account> GetAccount()
        {
            var user = Get();

            return await _accountManager.FindById(user.AccountId);
        }

        public async Task<Profile> GetProfile()
        {
            var user = Get();

            return await _profileRepo.FindById(user.ProfileId);
        }
    }
}