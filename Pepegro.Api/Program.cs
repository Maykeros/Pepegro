using System.Globalization;
using System.Text;
using Domain.DTO_s;
using Domain.Entities.Authorization;
using Domain.Entities.MailServiceEntities;
using Domain.Entities.MainEntities;
using Infrastructure;
using Infrastructure.Abstractions;
using Infrastructure.Abstractions.Authentication;
using Infrastructure.Abstractions.Repositories;
using Infrastructure.Authentication;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pepegro.Api.Extensions;
using Pepegro.Api.Middlewares;
using Pepegro.Bll.Services.MainServices;
using Pepegro.Bll.Services.MainServices.MailService;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Services.Services.Authentication;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        path: "/Users/turchynovychnazarii/Desktop/LogPath/log-.txt",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{level:u3}] {Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Information)
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    Log.Information("Application is Starting");

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGenWithJWT();

    builder.Services.AddTransient<IUnitOfWork, UnitOfWork>()
        .AddScoped<IOrderService, OrderService>()
        .AddScoped<IProductService, ProductService>()
        .AddTransient<IMailService, MailService>()
        .AddScoped<IAccountService, AccountService>()
        .AddJwtTokenGenerator(builder.Configuration)
        .AddJwtBearerOptions(builder.Configuration)
        .AddTransient<GlobalExceptionHandlingMiddleware>()
        .Configure<MailCredentials>(builder.Configuration.GetSection("MailCredentials"));
    
    builder.Services.AddDbContext<DataBaseContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    
    builder.Services
        .AddIdentity<User, Role>(options => options.PasswordSettings())
        .AddUserManager<UserManager<User>>()
        .AddEntityFrameworkStores<DataBaseContext>()
        .AddDefaultTokenProviders();

    builder.Services.AddAutoMapper(typeof(Program));
    
    
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled Exception");
}
finally
{
    Log.Information("shut down complete");
    Log.CloseAndFlush();
}