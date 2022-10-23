namespace Pepegro.Api.Controllers;

using Domain.DTO_s;
using Infrastructure.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Authentication;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAndSendConfirmationToken([FromBody] RegisterDTO registerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Something goes wrong");
        }
        await _accountService.RegisterAndSendConfirmationToken(registerDto);
        return Ok();

    }
    
    [HttpPost("confirm-register")]
    public async Task<IActionResult> ConfirmRegistration([FromQuery] int Id, [FromQuery] string token)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Something goes wrong");
        }
        await _accountService.ConfirmRegistration(Id,token);
        return Ok();

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        return Ok(await _accountService.Login(loginDto));
    }
}