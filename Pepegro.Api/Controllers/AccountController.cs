namespace Pepegro.Api.Controllers;

using Domain.DTO_s;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Authentication;

[Controller]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Something goes wrong");
        }
        await _accountService.Register(registerDto);
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