using System;

namespace Mublog.Server.PublicApi.Mock
{
    public class Post
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public string text { get; set; }
        public DateTime Datetime { get; set; }
    }
}