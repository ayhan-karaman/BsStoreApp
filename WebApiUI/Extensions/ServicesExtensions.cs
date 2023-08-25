using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
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
                services.AddScoped<ValidateMediaTypeAttribute>();
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


        public static void ConfigureDataShaper(this IServiceCollection services) => 
         services.AddScoped<IDataShaper<BookDto>, DataShaper<BookDto>>();

         public static void AddCustomMediaTypes(this IServiceCollection services)
         {
               services.Configure<MvcOptions>(config => {
                    var systemTextJsonOutputFormatter = config
                    .OutputFormatters
                    .OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();
                    if(systemTextJsonOutputFormatter is not null)
                    {
                          systemTextJsonOutputFormatter.SupportedMediaTypes
                          .Add("application/vnd.krmn.hateoas+json");
                          systemTextJsonOutputFormatter.SupportedMediaTypes
                          .Add("application/vnd.krmn.apiroot+json");
                    }
               
                    var xmlOutputFormatter = config
                    .OutputFormatters
                    .OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();
                    if(xmlOutputFormatter is not null)
                    {
                          xmlOutputFormatter
                          .SupportedMediaTypes
                          .Add("application/vnd.krmn.hateoas+xml");
                          xmlOutputFormatter.SupportedMediaTypes
                          .Add("application/vnd.krmn.apiroot+xml");
                    }
               });
         }

    }
}