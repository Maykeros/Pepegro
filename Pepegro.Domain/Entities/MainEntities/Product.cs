namespace Domain.Entities.MainEntities;

using Authorization;

public class Product
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public int Price { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public List<Order> Orders { get; set; }
}