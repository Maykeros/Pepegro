namespace Domain.DTO_s;

using System.ComponentModel.DataAnnotations;
using Entities.MainEntities;

public class UserDTO
{
    [Required] public string FirstName { get; set; }
    
    [Required] public string LastName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public List<Order> Orders { get; set; }
}