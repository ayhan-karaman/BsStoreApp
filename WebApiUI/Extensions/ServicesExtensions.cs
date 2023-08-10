using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EfCore;
using Services.Concretes;
using Services.Contracts;

namespace WebApiUI.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSQLContext(this IServiceCollection services, IConfiguration configuration)
        =>  services.AddDbContext<RepositoryContext>
        (options => options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));
       
       public static void ConfigureRepositoryManager(this IServiceCollection services)
       {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
       }
       public static void ConfigureServiceManager(this IServiceCollection services)
       {
            services.AddScoped<IServiceManager, ServiceManager>();
       }
    }
}