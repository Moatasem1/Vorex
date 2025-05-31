using Vorex.Domain.lib;

namespace Vorex.Domain.User.Specifications;

public class UserWithCryptoFavoriteAndCompareList : Specification<User>
{
    public UserWithCryptoFavoriteAndCompareList(Guid userId)
    {
        Criteria = u => u.Id == userId;
        AddInclude(u => u.Comparisons);
    }
}
public class UserWithCryptoCompare : Specification<User>
{
    public UserWithCryptoCompare(Guid userId)
    {
        Criteria = u => u.Id == userId;
        AddInclude(u => u.Comparisons);
    }
}

public class UserWithCryptoFavourite : Specification<User>
{
    public UserWithCryptoFavourite(Guid userId)
    {
        Criteria = u => u.Id == userId;
        AddInclude(u => u.Favorites);
    }
}
