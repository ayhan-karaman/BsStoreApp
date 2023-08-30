using NLog;
using WebApiUI.Extensions;
using Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Concretes;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
// Add services to the container.

builder.Services.AddControllers(config => {
     // İçerik pazarlığı client'in bizden istemiş olduğu dosya formatının dönüşü hakkında cevap vermek konusunda ki 
    // olumlu yada olumsuz durumu değerlendirdiğimiz alan config.RespectBrowserAcceptHeader
     config.RespectBrowserAcceptHeader = true;
     // Client'in bizden istemiş olduğu içerik formatına kapalı isek 406 hata kodunun gönderilmesi
     config.ReturnHttpNotAcceptable = true;
     config.CacheProfiles.Add("5mins", new CacheProfile(){Duration = 300});
})
.AddXmlDataContractSerializerFormatters() // Xml formatında çıkış yapmamızı onaylan yapıdır.
.AddCustomCsvFormatter()
.AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
;//.AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<ApiBehaviorOptions>(options => {
      options.SuppressModelStateInvalidFilter = true;
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.ConfigureSQLContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureDataShaper();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureActionFilters();
builder.Services.ConfigureCors();
builder.Services.AddCustomMediaTypes();
builder.Services.AddScoped<IBookLinks, BookLinks>();
builder.Services.ConfigureVersioning();
builder.Services.ConfigureResponseCaching();
builder.Services.ConfigureHttpCacheHeaders();
builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(sw => {
        sw.SwaggerEndpoint("/swagger/v1/swagger.json", "Krmn v1");
        sw.SwaggerEndpoint("/swagger/v2/swagger.json", "Krmn v2");
    });
}
if(app.Environment.IsProduction())
{
     app.UseHsts();
}

app.UseHttpsRedirection();
app.UseIpRateLimiting();
app.UseCors("CorsPolicy");

// Caching Middlewares
app.UseResponseCaching();
app.UseHttpCacheHeaders();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
