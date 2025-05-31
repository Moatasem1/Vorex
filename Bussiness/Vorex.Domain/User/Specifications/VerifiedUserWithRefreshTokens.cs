using Vorex.Domain.lib;

namespace Vorex.Domain.User.Specifications;


public class VerifiedUserWithRefreshTokens : Specification<User>
{

    public VerifiedUserWithRefreshTokens(string token)
    {
        Criteria = u => u.RefreshTokens.Any(r => r.Token == token && r.ExpiresOn > DateTime.Now);
        AddInclude(u => u.RefreshTokens);
    }
}

public class VerifiedUserByEmailWithRefreshTokens : Specification<User>
{
    public VerifiedUserByEmailWithRefreshTokens(string email)
    {
        Criteria = u => u.Email == email && u.IsEmailConfirmed;
        AddInclude(u => u.RefreshTokens);
    }
}
