using Mublog.Server.PublicApi.Validators.Interfaces;

namespace Mublog.Server.PublicApi.Validators
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