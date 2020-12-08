namespace Mublog.Server.PublicApi.V1.DTOs.Users
{
    public class UserResponseDto
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string ProfileImageId { get; set; }
        public string HeaderImageId { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
    }
}