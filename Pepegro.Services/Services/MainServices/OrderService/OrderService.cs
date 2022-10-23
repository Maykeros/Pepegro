namespace Pepegro.Bll.Services.MainServices;

using AutoMapper;
using Domain.DTO_s.MainEntities;
using Domain.Entities.Authorization;
using Domain.Entities.MainEntities;
using Infrastructure.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Serilog;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;


    public OrderService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<List<GetOrderDto>> GetAllOrders(int userId)
    {
        Log.Logger.Information($"Try to get all orders of user by id:{userId}");

        var result = await _userManager.FindByIdAsync(userId.ToString());
        
        if (result == null)
        {
            throw new Exception("User doesn't exists");
        }
        
        var orders = await _unitOfWork.Orders.GetAll(o => o.Id == userId);
        

        return _mapper.Map<List<GetOrderDto>>(orders);
    }

    public async Task<GetOrderDto> GetOrderById(int id)
    {
        Log.Logger.Information($"Try to get order of id:{id}");

        var order = await _unitOfWork.Orders.Get(o => o.Id == id);

        return _mapper.Map<GetOrderDto>(order);
    }

    public async Task<int> CreateOrder(CreateOrderDTO createOrderDto)
    {
        Log.Logger.Information($"Try to create order");

        var order = _mapper.Map<Order>(createOrderDto);

        await _unitOfWork.Orders.Create(order);

        return order.Id;
    }


    public async Task DeleteOrder(int id)
    {
        Log.Logger.Information($"Try to delete order");

        await _unitOfWork.Orders.Delete(id);
    }
}