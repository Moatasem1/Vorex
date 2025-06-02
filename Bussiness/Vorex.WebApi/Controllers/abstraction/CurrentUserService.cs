using System.Security.Claims;

namespace Vorex.WebApi.Controllers.abstraction;

public class CurrentUserService(IHttpContextAccessor _context)
{
    public Guid? UserId
    {
        get
        {
            var userIdClaim = _context.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return userIdClaim!=null ? Guid.Parse(userIdClaim) : null;
        }
    }
}
