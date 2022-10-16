namespace Pepegro.Api.Extensions;

using Microsoft.AspNetCore.Identity;

public static class IdentityOptionsExtensions
{
    public static void PasswordSettings(this IdentityOptions options)
    {
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredUniqueChars = 1;
        options.Password.RequiredLength = 1;
    }
}