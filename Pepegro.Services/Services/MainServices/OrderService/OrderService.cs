namespace Pepegro.Bll.Services.MainServices;

using AutoMapper;
using Domain.DTO_s.MainEntities;
using Domain.Entities.MainEntities;
using Infrastructure.Abstractions;
using Microsoft.Extensions.Logging;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderService> _logger;
    private readonly IMapper _mapper;


    public OrderService(IUnitOfWork unitOfWork, ILogger<OrderService> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<GetOrderDto>> GetAllOrders(int userId)
    {
        _logger.LogInformation($"Try to get all orders of user by id:{userId}");

        var orders = await _unitOfWork.Orders.GetAll(o => o.Id == userId);

        if (orders == null)
        {
            throw new Exception("User doesn't exists");
        }

        return _mapper.Map<List<GetOrderDto>>(orders);
    }

    public async Task<GetOrderDto> GetOrderById(int id)
    {
        _logger.LogInformation($"Try to get order of id:{id}");

        var order = await _unitOfWork.Orders.Get(o => o.Id == id);

        return _mapper.Map<GetOrderDto>(order);
    }

    public async Task<int> CreateOrder(CreateOrderDTO createOrderDto)
    {
        _logger.LogInformation($"Try to create order");

        var order = _mapper.Map<Order>(createOrderDto);

        await _unitOfWork.Orders.Create(order);

        return order.Id;
    }


    public async Task DeleteOrder(int id)
    {
        _logger.LogInformation($"Try to delete order");

        await _unitOfWork.Orders.Delete(id);
    }
}