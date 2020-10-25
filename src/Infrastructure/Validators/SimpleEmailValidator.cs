using Mublog.Server.Infrastructure.Validators.Interfaces;

namespace Mublog.Server.Infrastructure.Validators
{
    public class SimpleEmailValidator : IEmailValidator
    {
        public bool IsValid(string email)
        {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch {
                return false;
            }
        }
    }
}