using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.PublicApi.Common.DTOs.V1.Posts;
using Mublog.Server.PublicApi.Common.DTOs.V1.Users;

namespace Mublog.Server.PublicApi.Config
{
    public class Mappings : AutoMapper.Profile
    {
        public Mappings()
        {
            CreateMap<PostCreateRequestDto, Post>();
            CreateMap<Profile, UserResponseDto>();
            CreateMap<Profile, PostUserResponseDto>();
            CreateMap<Post, PostResponseDto>();
        }
    }
}