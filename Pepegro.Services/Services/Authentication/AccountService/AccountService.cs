namespace Services.Services.Authentication;

using System.Text;
using AutoMapper;
using Domain.DTO_s;
using Domain.Entities.Authorization;
using Domain.Entities.MailServiceEntities;
using Infrastructure.Abstractions.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Pepegro.Bll.Services.MainServices.MailService;
using Serilog;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IMailService _mailService;

    public AccountService(UserManager<User> userManager,
        IMapper mapper,
        IJwtTokenGenerator tokenGenerator,
        IMailService mailService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _tokenGenerator = tokenGenerator;
        _mailService = mailService;
    }

    public async Task<IActionResult> RegisterAndSendConfirmationToken(RegisterDTO registerDto)
    {
        Log.Logger.Information($"Registration attempt for {registerDto.Email}");

        var user = _mapper.Map<User>(registerDto);

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            Log.Logger.Error("Fail to create user");
            return new BadRequestResult();
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        
        var encodeToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        var url = $"https://localhost:7215/confirm-register?id={user.Id}&token={encodeToken}";
        
        _mailService.SendEmail(new MailInformation()
        {
            To = user.Email,
            Subject = "Confirm your registration",
            Message = url
        });
        return new OkResult();
    }

    public async Task<IActionResult> ConfirmRegistration(int userId ,string token)
    {
        Log.Logger.Information("Try to confirm registration");

        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            Log.Logger.Error("User doesn't exist");
            return new BadRequestResult();
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded)
        {
            Log.Error($"Fail to confirm registration to {user.Email}");
        }

        await _userManager.AddToRoleAsync(user, Roles.User.ToString());
        
        return new OkResult();
    }
    
    public async Task<string> Login(LoginDTO loginDTO)
    {
        Log.Logger.Information($"Login attempt for {loginDTO.Email}");

        var user = await _userManager.FindByEmailAsync(loginDTO.Email);

        var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

        if (!result)
        {
            return "bad request";
        }

        var roles = await _userManager.GetRolesAsync(user);

        var token = _tokenGenerator.GenerateToken(user, roles.FirstOrDefault(r => true)!);

        return token;
    }
}