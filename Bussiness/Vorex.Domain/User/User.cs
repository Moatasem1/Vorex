using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vorex.Domain.Common;
using Vorex.Domain.Common.Interfaces;
using Vorex.Domain.CryptoAnalyses;

namespace Vorex.Domain.User;

public class User : BaseEntity, IAggregateRoot
{
    private User() {

    }

    private User(string firstName, string lastName, string email, string password, string profileImage)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        ProfileImage = profileImage;
    }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; private set; }
    [Required]
    [MaxLength(50)]
    public string LastName { get; private set; }
    [Required]
    [EmailAddress]
    public string Email { get; private set; }
    [Required]
    [MaxLength(100)]
    public string Password { get; private set; }

    [MaxLength(80)]
    public string? ProfileImage { get; private set; }

    private readonly List<CryptoFavorite> _favorites = new();
    public virtual IReadOnlyCollection<CryptoFavorite> Favorites => _favorites.AsReadOnly();

    private readonly List<CryptoComparison> _comparisons = new();
    public IReadOnlyCollection<CryptoComparison> Comparisons => _comparisons.AsReadOnly();
}
