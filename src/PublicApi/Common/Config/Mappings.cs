using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.PublicApi.V1.DTOs.Posts;
using Mublog.Server.PublicApi.V1.DTOs.Users;

namespace Mublog.Server.PublicApi.Common.Config
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