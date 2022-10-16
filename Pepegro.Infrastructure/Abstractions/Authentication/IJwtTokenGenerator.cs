namespace Infrastructure.Abstractions.Authentication;

using Domain.Entities.Authorization;

public interface IJwtTokenGenerator
{
    public string GenerateToken(User user, string role);
}