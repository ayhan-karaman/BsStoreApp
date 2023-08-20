using NLog;
using WebApiUI.Extensions;
using Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
// Add services to the container.

builder.Services.AddControllers(config => {
     // İçerik pazarlığı client'in bizden istemiş olduğu dosya formatının dönüşü hakkında cevap vermek konusunda ki 
    // olumlu yada olumsuz durumu değerlendirdiğimiz alan config.RespectBrowserAcceptHeader
     config.RespectBrowserAcceptHeader = true;
     // Client'in bizden istemiş olduğu içerik formatına kapalı isek 406 hata kodunun gönderilmesi
     config.ReturnHttpNotAcceptable = true;
})
.AddCustomCsvFormatter()
.AddXmlDataContractSerializerFormatters() // Xml formatında çıkış yapmamızı onaylan yapıdır.
.AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
.AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<ApiBehaviorOptions>(options => {
      options.SuppressModelStateInvalidFilter = true;
});



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureSQLContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureActionFilters();
builder.Services.ConfigureCors();


var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if(app.Environment.IsProduction())
{
     app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
