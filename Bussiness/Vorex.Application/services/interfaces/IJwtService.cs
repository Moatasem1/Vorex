using System.Security.Claims;
using Vorex.Domain.User;

namespace Vorex.Application.services.interfaces;

public interface IJwtService
{
    string GenerateEmailVerificationToken(Guid userId,string email);

     ClaimsPrincipal? ValidateToken(string token);

    string GenerateToken(IEnumerable<Claim> claims, DateTime expires);

    string GenerateRefreshToken();

    string GenerateAccessToken(User user);
}
