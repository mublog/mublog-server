using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.PublicApi.Common.DTOs.V1.Posts;
using Mublog.Server.PublicApi.Common.DTOs.V1.Users;
using Profile = AutoMapper.Profile;

namespace Mublog.Server.PublicApi.Config
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<PostCreateRequestDto, Post>();
            CreateMap<Mublog.Server.Domain.Data.Entities.Profile, UserResponseDto>();
            CreateMap<Mublog.Server.Domain.Data.Entities.Profile, PostUserResponseDto>();
            CreateMap<Post, PostResponseDto>();
        }
    }
}