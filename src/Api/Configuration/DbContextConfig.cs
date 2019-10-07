using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Configuration
{
    public static class DbContextConfig
    {
        public static void RegistrationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IFactoryConnection>(new NpgSqlFactory(configuration.GetConnectionString("db")));
            services.AddTransient<ISearchRepository, SearchRepository>();
        }
    }
}