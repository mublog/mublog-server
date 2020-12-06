using Mublog.Server.Domain.Common.Helpers;
using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.Infrastructure.Common.Config
{
    public class Mappings : AutoMapper.Profile
    {
        public Mappings()
        {
            CreateMap<Post, PostWithLike>();
        }
    }
}