using System.Security.Claims;

namespace Vorex.WebApi.Controllers.abstraction;

public class CurrentUserService(IHttpContextAccessor _context)
{
    public Guid UserId
    {
        get
        {
            var userIdClaim = _context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Guid.Parse("2519a67d-0de0-4b4b-9733-88d994876051"); // fake user till we implement the authentication

            return Guid.Parse(userIdClaim);
        }
    }
}
