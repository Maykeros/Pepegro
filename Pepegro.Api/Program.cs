using System.Text;
using Domain.DTO_s;
using Domain.Entities.Authorization;
using Infrastructure;
using Infrastructure.Abstractions.Authentication;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pepegro.Api.Extensions;
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
    builder.Services.AddSwaggerGen();

    builder.Services
        .AddJwtTokenGenerator(builder.Configuration)
        .AddJwtBearerOptions(builder.Configuration);

    builder.Services.AddScoped<IAccountService, AccountService>();
    
    builder.Services
        .AddIdentity<User, Role>(options => options.PasswordSettings())
        .AddUserManager<UserManager<User>>()
        .AddEntityFrameworkStores<DataBaseContext>()
        .AddDefaultTokenProviders();
    
    builder.Services.AddDbContext<DataBaseContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddAutoMapper(typeof(Program));

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled Exeption");
}
finally
{
    Log.Information("shut down complete");
    Log.CloseAndFlush();
}