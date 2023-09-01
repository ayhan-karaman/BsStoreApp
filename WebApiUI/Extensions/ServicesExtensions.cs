using System.Text;
using AspNetCoreRateLimit;
using Entities.DataTransferObjects;
using Entities.Models;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation.ActionFilters;
using Presentation.Controllers;
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

            public static void ConfigureVersioning(this IServiceCollection services)
         {
              services.AddApiVersioning( options => {
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                    options.Conventions.Controller<BooksController>().HasApiVersion(new ApiVersion(1, 0));
                    options.Conventions.Controller<BooksV2Controller>().HasDeprecatedApiVersion(new ApiVersion(2, 0));
              });
         }
         
            public static void ConfigureResponseCaching(this IServiceCollection services) =>
               services.AddResponseCaching();
            public static void ConfigureHttpCacheHeaders(this IServiceCollection services) =>
               services.AddHttpCacheHeaders(expirationOpt => {
                     expirationOpt.MaxAge = 90;
                     expirationOpt.CacheLocation =CacheLocation.Public;
               }, 
                validationOpt => { validationOpt.MustRevalidate = false; }
               );
         
            public static void ConfigureRateLimitingOptions(this IServiceCollection services)
      {
             var rateLimitRules = new List<RateLimitRule>()
             {
                   new RateLimitRule()
                   {
                        Endpoint = "*",
                        Limit = 60,
                        Period = "1m"
                   }
             };
              services.Configure<IpRateLimitOptions>(opt => {
                  opt.GeneralRules = rateLimitRules;
              });
              services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
              services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
              services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
              services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
      }
    
            public static void ConfigureIdentity(this IServiceCollection services)
      {
             var builder = services.AddIdentity<User, IdentityRole>(opt => {
                   opt.Password.RequireDigit = false;
                   opt.Password.RequireDigit = false;
                   opt.Password.RequireUppercase = false;
                   opt.Password.RequireNonAlphanumeric = false;
                   opt.Password.RequiredLength = 1;


                   opt.User.RequireUniqueEmail = true;
             })
             .AddEntityFrameworkStores<RepositoryContext>()
             .AddDefaultTokenProviders();
      }
      
            public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
      {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["secretKey"];

            services.AddAuthentication(opt => {
                   opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(opt => {
                  opt.TokenValidationParameters = new TokenValidationParameters
                  {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["validIssuer"],
                        ValidAudience = jwtSettings["validAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                  };
            });
      }
    
      public static void ConfigureSwagger(this IServiceCollection services)
      {
             services.AddSwaggerGen(sw => {
                   sw.SwaggerDoc("v1", new OpenApiInfo 
                   {
                        Title = "Krmn", 
                        Version = "v1", 
                        Description = "BTK Akademi  ASP.NET Core Web API",
                        TermsOfService = new Uri("https://www.btkakademi.gov.tr/"),
                         Contact = new OpenApiContact
                         {
                               Name = "Zafer CÖMERT",
                               Email = "comertzafer@gmail.com",
                               Url = new Uri("http://www.zafercomert.com/")
                         }
                        });
                   sw.SwaggerDoc("v2", new OpenApiInfo {Title = "Krmn", Version = "v2"});

                   sw.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
                        In = ParameterLocation.Header,
                        Description = "Place to add JWT with Bearer",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                   });

                   sw.AddSecurityRequirement(new OpenApiSecurityRequirement() 
                   {
                        {
                              new OpenApiSecurityScheme
                              {
                                    Reference = new OpenApiReference
                                    {
                                          Type = ReferenceType.SecurityScheme,
                                          Id = "Bearer"
                                    },
                                     Name = "Bearer"
                                    
                              },
                              new List<string>()
                         }
                   });
             });

      }
    
     public static void RegisterRepository(this IServiceCollection services)
     {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
     }
      public static void RegisterServices(this IServiceCollection services)
      {
             services.AddScoped<IBookService, BookManager>();
             services.AddScoped<ICategoryService, CategoryManager>();
             services.AddScoped<IAuthenticationService, AuthenticationManager>();
      }
      
    }
}