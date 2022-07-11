using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using StudentManagement.Database;
using StudentManagement.Middleware;
using StudentManagement.Services;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{

    var builder = WebApplication.CreateBuilder(args);

    ConfigureServices(builder.Services, builder.Configuration);

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add services to the container.
    // NLog: Setup NLog for Dependency injection

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();


    var app = builder.Build();
   

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}






void ConfigureServices(IServiceCollection services,ConfigurationManager configManager)
{    
    services.Configure<StudentStoreDatabaseSettings>(
                 configManager.GetSection(nameof(StudentStoreDatabaseSettings)));


    services.AddSingleton<IStudentStoreDatabaseSettings>(sp =>
        sp.GetRequiredService<IOptions<StudentStoreDatabaseSettings>>().Value);

    services.AddSingleton<IMongoClient>(s =>
            new MongoClient(configManager.GetValue<string>("StudentStoreDatabaseSettings:ConnectionString")));

    services.AddScoped<IStudentService, StudentService>();
}