namespace Domain.Entities.Authorization;

using MainEntities;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<Product>? Products { get; set; }
    public List<Order>? Orders { get; set; }
}