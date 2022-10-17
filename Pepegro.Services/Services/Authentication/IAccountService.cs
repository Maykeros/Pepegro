namespace Services.Services.Authentication;

using Domain.DTO_s;
using Microsoft.AspNetCore.Mvc;

public interface IAccountService
{
    public Task<IActionResult> Register(RegisterDTO registerDto);

    public Task<string> Login(LoginDTO loginDTO);
}