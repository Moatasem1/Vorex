using Vorex.Domain.lib;

namespace Vorex.Domain.User.Specifications;


public class VerifiedUserWithRefreshTokens : Specification<User>
{
    public VerifiedUserWithRefreshTokens(string email , string password)
    {
        Criteria = u => u.Email == email && u.Password==password && u.IsEmailConfirmed;
        AddInclude(u => u.RefreshTokens);
    }

    public VerifiedUserWithRefreshTokens(string token)
    {
        Criteria = u => u.RefreshTokens.Any(r => r.Token == token && r.ExpiresOn > DateTime.Now);
        AddInclude(u => u.RefreshTokens);
    }
}
