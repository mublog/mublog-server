namespace Mublog.Server.PublicApi.Common.DTOs.V1.Users
{
    public class UserResponseDto
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string ProfileImageId { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
    }
}