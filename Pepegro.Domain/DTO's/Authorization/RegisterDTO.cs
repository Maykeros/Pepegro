namespace Domain.DTO_s;

using System.ComponentModel.DataAnnotations;
using Entities.MainEntities;

public class RegisterDTO
{
    [Required] public string FirstName { get; set; }
    
    [Required] public string LastName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public string UserName => Email;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}