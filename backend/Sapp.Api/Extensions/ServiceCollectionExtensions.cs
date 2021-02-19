using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Sapp.Core.Persistence;

namespace Sapp.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString;
            var herokuDatabaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            if (string.IsNullOrWhiteSpace(herokuDatabaseUrl))
            {
                connectionString = configuration.GetConnectionString("Postgres");
            }
            else
            {
                //parse database URL. Format is postgres://<username>:<password>@<host>/<dbname>
                var uri = new Uri(herokuDatabaseUrl);
                var username = uri.UserInfo.Split(':')[0];
                var password = uri.UserInfo.Split(':')[1];

                connectionString = new NpgsqlConnectionStringBuilder
                {
                    Host = uri.Host,
                    Database = uri.AbsolutePath.Substring(1),
                    Username = username,
                    Password = password,
                    Port = uri.Port,
                    SslMode = SslMode.Require,
                    TrustServerCertificate = true
                }.ConnectionString;
            }

            services.AddDbContext<ApiContext>(
                b => b.UseNpgsql(connectionString, o => o.MigrationsAssembly("Sapp.Api")));

            return services;
        }
    }
}