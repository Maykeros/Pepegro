namespace Pepegro.Api.Extensions;

using System.Text;
using Infrastructure.Abstractions.Authentication;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

public static class AuthenticationExt
{
    public static IServiceCollection AddJwtTokenGenerator(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }

    public static IServiceCollection AddJwtBearerOptions(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var configurationSettings = new JwtSettings();
        configuration.Bind("JwtSettings", configurationSettings);
        
        services.AddAuthentication(options =>
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configurationSettings.Issuer,
                    ValidAudience = configurationSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationSettings.Secret)),
                };
            });
        return services;
    }
}