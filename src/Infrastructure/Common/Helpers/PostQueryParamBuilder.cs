using Mublog.Server.Domain.Data;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Infrastructure.Common.Helpers
{
    public static class PostQueryParamBuilder
    {
        public static PostQueryParameters GetInternalParams(this ExternalPostQueryParameters externalParams, Profile profile)
        {
            return new PostQueryParameters
            {
                Page = externalParams.Page,
                Size = externalParams.Size,
                Profile = profile
            };
        }
    }
}