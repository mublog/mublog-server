namespace Mublog.Server.Infrastructure.Validators.Interfaces
{
    public interface IValidator<T>
    {
        bool IsValid(T obj);
    }
}