namespace Services.Services.Authentication;

using AutoMapper;
using Domain.DTO_s;
using Domain.Entities.Authorization;
using Infrastructure.Abstractions.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<AccountService> _logger;
    private readonly IMapper _mapper;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public AccountService(UserManager<User> userManager, ILogger<AccountService> logger, IMapper mapper, IJwtTokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _logger = logger;
        _mapper = mapper;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<IActionResult> Register(RegisterDTO registerDto)
    {
        _logger.LogInformation($"Registration attempt for {registerDto.Email}");

        var user = _mapper.Map<User>(registerDto);

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            return new BadRequestResult();
        }

        await _userManager.AddToRoleAsync(user, Roles.User.ToString());
        
        return new OkResult();
    }
    
    public async Task<string> Login(LoginDTO loginDTO)
    {
        _logger.LogInformation($"Login attempt for {loginDTO.Email}");

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