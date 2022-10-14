namespace Domain.Entities.MainEntities;

public class Product
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public int Price { get; set; }

    public int SellerId { get; set; }

    public Seller Seller { get; set; }

    public List<Order> Orders { get; set; }
}