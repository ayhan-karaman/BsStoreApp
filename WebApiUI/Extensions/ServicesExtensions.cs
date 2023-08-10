using Microsoft.EntityFrameworkCore;
using Repositories.EfCore;

namespace WebApiUI.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSQLContext(this IServiceCollection services, IConfiguration configuration)
        =>  services.AddDbContext<RepositoryContext>
        (options => options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));
        
    }
}