
namespace Mublog.Server.Domain.Data
{
    public class PostQueryParameters : QueryParameters
    {
        private string _username;

        public string Username
        {
            get => _username;
            set => _username = value.ToLower();
        }
    }
}