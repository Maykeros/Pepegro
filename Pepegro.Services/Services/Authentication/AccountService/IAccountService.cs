namespace Services.Services.Authentication;

using Domain.DTO_s;
using Microsoft.AspNetCore.Mvc;

public interface IAccountService
{
    public Task<IActionResult> RegisterAndSendConfirmationToken(RegisterDTO registerDto);

    public Task<IActionResult> ConfirmRegistration(int userId, string token);

    public Task<string> Login(LoginDTO loginDTO);
}