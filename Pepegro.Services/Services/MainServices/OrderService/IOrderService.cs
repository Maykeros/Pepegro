namespace Pepegro.Bll.Services.MainServices;

using Domain.DTO_s.MainEntities;

public interface IOrderService
{
    public Task<List<GetOrderDto>> GetAllOrders(int userId);
    
    public Task<GetOrderDto> GetOrderById(int id);

    public Task<int> CreateOrder(CreateOrderDTO getOrderDto);

    public Task DeleteOrder(int id);
}