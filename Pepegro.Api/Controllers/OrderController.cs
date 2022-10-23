namespace Pepegro.Api.Controllers;

using Bll.Services.MainServices;
using Domain.DTO_s.MainEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;


    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetUserOrders([FromQuery] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Parameters is not valid");
        }
        return Ok(await _orderService.GetAllOrders(id));
    }
    [HttpGet("GetById")]
    public async Task<IActionResult> GetOrderById([FromQuery] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Parameters is not valid");
        }

        return Ok(await _orderService.GetOrderById(id));
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO orderDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Parameters is not valid");
        }

        return Ok(await _orderService.CreateOrder(orderDto));
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteProduct([FromQuery] int orderId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Parameters is not valid");
        }

        await _orderService.DeleteOrder(orderId);
      
        return Ok();
    }
}