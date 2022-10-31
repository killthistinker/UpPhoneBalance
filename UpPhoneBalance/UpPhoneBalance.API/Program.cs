using System.Globalization;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using UpPhoneBalance.Application;
using UpPhoneBalance.Infrastructure;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    var services = builder.Services; 
// Adding Nlog     
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    
    
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(); 
// Adding DependencyInjections       
    services.AddInfrastructureServices(builder.Configuration);
    services.AddApplicationServices();
    
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "UpPhoneBalance.API", Version = "v1" });
        c.EnableAnnotations();
    });
    var app = builder.Build();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
// Localization settings
    var supportedCultures = new[]
    {
        new CultureInfo("kk"),
        new CultureInfo("ru")
    };
    app.UseRequestLocalization(new RequestLocalizationOptions
    {
        SupportedCultures = supportedCultures,
        SupportedUICultures = supportedCultures
    });
    
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    logger.Error(e, "Остановка программы");
}
finally
{
    NLog.LogManager.Shutdown();
}
public partial class Program { }
