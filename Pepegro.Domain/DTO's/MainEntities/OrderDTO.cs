namespace Domain.DTO_s.MainEntities;

using Entities.Authorization;
using Entities.MainEntities;

public class OrderDTO
{
    public int Id { get; set; }

    public User User { get; set; }

    public Product Product { get; set; }
}