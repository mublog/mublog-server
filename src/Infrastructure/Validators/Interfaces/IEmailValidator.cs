namespace Mublog.Server.Infrastructure.Validators.Interfaces
{
    public interface IEmailValidator : IValidator<string>
    {
        new bool IsValid(string email);
    }
}