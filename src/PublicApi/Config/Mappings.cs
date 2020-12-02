using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.PublicApi.Common.DTOs.V1.Posts;
using Profile = AutoMapper.Profile;

namespace Mublog.Server.PublicApi.Config
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<PostCreateRequestDto, Post>();
        }
    }
}