namespace Mublog.Server.PublicApi.Validators.Interfaces
{
    public interface IValidator<T>
    {
        bool IsValid(T obj);
    }
}