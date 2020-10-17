namespace Mublog.Server.PublicApi.Validators.Interfaces
{
    public interface IEmailValidator : IValidator<string>
    {
        new bool IsValid(string email);
    }
}