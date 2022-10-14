namespace Domain.Entities.MainEntities;

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class Seller
{
    public Seller()
    {
        ProductAmount = Products.Count;
    }
    [Required] 
    public string FirstName { get; set; }
    
    [Required] 
    public string LastName { get; set; }

    public int ProductAmount { get; set; }

    public List<Product> Products { get; set; }
}