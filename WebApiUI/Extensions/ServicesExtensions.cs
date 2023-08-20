using Microsoft.EntityFrameworkCore;
using Presentation.ActionFilters;
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

       public static void ConfigureActionFilters(this IServiceCollection services)
       {
                services.AddScoped<ValidationFilterAttribute>();
                services.AddSingleton<LogFilterAttritbute>();
       }
       
       public static void ConfigureCors(this IServiceCollection services)
       {
           services.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder => 
                 builder.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .WithExposedHeaders("X-Pagination")
                 );
           });
       }
       public static void ConfigureLoggerService(this IServiceCollection services) => 
        services.AddSingleton<ILoggerService, LoggerManager>();
    }
}