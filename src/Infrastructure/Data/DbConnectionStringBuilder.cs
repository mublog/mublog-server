using System;

namespace Mublog.Server.PublicApi.Data
{
    public class DbConnectionStringBuilder
    {
        public static string Build()
        {
            var host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
            if (string.IsNullOrWhiteSpace(host)) throw new Exception("Environment variable POSTGRES_HOST was not found or is empty.");
            
            var port = Environment.GetEnvironmentVariable("POSTGRES_PORT");
            if (string.IsNullOrWhiteSpace(host)) throw new Exception("Environment variable POSTGRES_PORT was not found or is empty.");
            
            var database = Environment.GetEnvironmentVariable("POSTGRES_DB");
            if (string.IsNullOrWhiteSpace(database)) throw new Exception("Environment variable POSTGRES_DB was not found or is empty.");
            
            var user = Environment.GetEnvironmentVariable("POSTGRES_USER");
            if (string.IsNullOrWhiteSpace(user)) throw new Exception("Environment variable POSTGRES_USER was not found or is empty.");
            
            var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
            if (string.IsNullOrWhiteSpace(user)) throw new Exception("Environment variable POSTGRES_USER was not found or is empty.");
            
            return $"Server={host};Port={port};Database={database};User Id={user};Password={password};";
        }
    }
}