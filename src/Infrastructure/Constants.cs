using System;

namespace Mublog.Server.Infrastructure
{
    /// <summary>
    /// Temporary, only for dev
    /// </summary>
    [Obsolete]
    public static class Constants
    {
        public const string Issuer = Audience;
        public const string Audience = "http://localhost:5000/";
        public const string Secret = "not_a_short_secret_to_avoid_any_erros_that_might_occur_otherwise";
    }
}