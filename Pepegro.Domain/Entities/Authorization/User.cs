namespace Domain.Entities.Authorization;

using System.ComponentModel.DataAnnotations;
using MainEntities;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public override string Email { get; set; }

    public List<Order> Orders { get; set; }
}