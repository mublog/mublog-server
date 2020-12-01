using System;

namespace Mublog.Server.Infrastructure.Common.Config
{
    public class DbConnectionStringBuilder // : IDbConnectionStringBuilder
    {
        public static string Build()
        {
            var host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
            if (string.IsNullOrWhiteSpace(host)) host = "localhost";

            var port = Environment.GetEnvironmentVariable("POSTGRES_PORT");
            if (string.IsNullOrWhiteSpace(port)) port = "5432";

            var database = Environment.GetEnvironmentVariable("POSTGRES_DB");
            if (string.IsNullOrWhiteSpace(database))
                return null;

            var user = Environment.GetEnvironmentVariable("POSTGRES_USER");
            if (string.IsNullOrWhiteSpace(user))
                return null;

            var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
            if (string.IsNullOrWhiteSpace(user))
                return null;

            return $"Server={host};Port={port};Database={database};Profile Id={user};Password={password};";
        }
    }
}