using Mublog.Server.Domain.Data.Entities;

namespace Mublog.Server.PublicApi.V1.DTOs.Posts
{
    public class PostUserResponseDto
    {
        public string Alias { get; set; }
        public string DisplayName { get; set; }
        public string ProfileImageUrl { get; set; }

        public PostUserResponseDto(Profile profile)
        {
            Alias = profile.Username;
            DisplayName = profile.DisplayName;
            ProfileImageUrl = profile.ProfileImage?.PublicId.ToString() ?? "";
        }
        
        public PostUserResponseDto()
        {
            
        }
    }
}