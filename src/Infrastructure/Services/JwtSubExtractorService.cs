using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;
using Mublog.Server.Infrastructure.Common.Helpers;
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

        public string GetUsername()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var username = string.Empty;

            if (user != null && user.HasClaim(c =>
                c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"))
                username = user.Claims.FirstOrDefault(c =>
                    c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            return username;
        }

        public async Task<Account> GetAccount()
        {
            var username = GetUsername();

            var account = await _accountManager.FindByUsername(username);

            return account;
        }

        public async Task<Profile> GetProfile()
        {
            var username = GetUsername();

            return await _profileRepo.GetByUsername(username);
        }
    }
}