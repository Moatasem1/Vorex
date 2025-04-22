using Vorex.Domain.lib;

namespace Vorex.Domain.User.Specifications;

public class UserWithCryptoFavoriteAndCompareList : Specification<User>
{
    public UserWithCryptoFavoriteAndCompareList(Guid userId)
    {
        Criteria = u => u.Id == userId;
        //AddInclude(u => u.Comparisons);
        //AddInclude(u => u.Favorites);
    }
}
