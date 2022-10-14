namespace Domain.Entities.Authorization;

using System.ComponentModel.DataAnnotations;
using MainEntities;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int>
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public override string Email { get; set; }

    public List<Order> Orders { get; set; }
}