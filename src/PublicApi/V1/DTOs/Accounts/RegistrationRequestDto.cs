using System.ComponentModel.DataAnnotations;

namespace Mublog.Server.PublicApi.V1.DTOs.Accounts
{
    public class RegistrationRequestDto
    {
        private string _username;

        [Required]
        public string Email { get; set; }

        [Required]
        public string Username
        {
            get => _username;
            set => _username = value.ToLower();
        }

        [Required]
        public string DisplayName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}