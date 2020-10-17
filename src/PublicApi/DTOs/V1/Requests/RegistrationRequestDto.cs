namespace Mublog.Server.PublicApi.DTOs.V1.Requests
{
    public class RegistrationRequestDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}