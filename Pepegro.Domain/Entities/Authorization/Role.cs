namespace Domain.Entities.Authorization;

using Microsoft.AspNetCore.Identity;

public class Role : IdentityRole<int>
{
    
}

public enum Roles
{
    User = 1,
    Admin = 2
}