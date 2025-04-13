using System.ComponentModel.DataAnnotations;
using Vorex.Domain.Common;
using Vorex.Domain.Common.Interfaces;
using Vorex.Domain.lib;

namespace Vorex.Domain.User;

public class User : BaseEntity, IAggregateRoot
{
    private User() {

    }

    private User(string firstName, string lastName, string email, string password, string? profileImage)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        ProfileImage = profileImage;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string? ProfileImage { get; private set; }

    private readonly List<CryptoFavorite> _favorites = new();
    public virtual IReadOnlyCollection<CryptoFavorite> Favorites => _favorites.AsReadOnly();

    private readonly List<CryptoComparison> _comparisons = new();
    public IReadOnlyCollection<CryptoComparison> Comparisons => _comparisons.AsReadOnly();

    public static class Factory
    {
        public static Result<User,Error> Create(string firstName, string lastName, string email, string password, string? profileImage)
        {
            var nameValidation = ValidateName(firstName, nameof(FirstName));
            if (nameValidation.IsFailure) return nameValidation.Error;

            nameValidation = ValidateName(lastName, nameof(LastName));
            if (nameValidation.IsFailure) return nameValidation.Error;

            var emailValidation = ValidateEmail(email);
            if (emailValidation.IsFailure) return emailValidation.Error;

            var passwordValidation = ValidatePassword(password);
            if (passwordValidation.IsFailure) return passwordValidation.Error;

            var user = new User(firstName, lastName, email, password, profileImage);
            return user;
        }
    }

    public Result<bool, Error> ChangePassword(string oldPassword, string newPassword)
    {
        var newPasswordValidation = ValidatePassword(newPassword);

        if (newPasswordValidation.IsFailure)
            return newPasswordValidation.Error;

        if (oldPassword != Password)
            return Error.ValueInvalid(nameof(User), nameof(Password));

        Password = newPassword;

        return true;
    }

    public Result<bool, Error> ChangeProfileImage(string? newProfileImage)
    {
        ProfileImage = newProfileImage;
        return true;
    }

    public Result<bool, Error> AddToFavorites(CryptoFavorite favorite)
    {
        if (IsCryptoFavoriteExists(favorite.CryptoAnalysisHistoryId))
            return Error.ValueAlreadyExists(nameof(CryptoFavorite), nameof(CryptoFavorite.CryptoAnalysisHistoryId), favorite.CryptoAnalysisHistoryId.ToString());

        _favorites.Add(favorite);
        return true;
    }

    public Result<bool, Error> RemoveFromFavorites(Guid cryptoHistoryIdToRemove)
    {
        var favoriteToRemove = _favorites.FirstOrDefault(x => x.CryptoAnalysisHistoryId == cryptoHistoryIdToRemove);

        if (favoriteToRemove is null)
            return Error.NotFound(nameof(CryptoFavorite),nameof(CryptoFavorite.CryptoAnalysisHistoryId), cryptoHistoryIdToRemove.ToString());

        _favorites.Remove(favoriteToRemove);
        return true;
    }

    public Result<bool, Error> AddToCompareList(CryptoComparison comparison)
    {
        if (IsCryptoInCompareList(comparison.CryptoAnalysisHistoryId))
            return Error.ValueAlreadyExists(nameof(User), nameof(comparison), comparison.CryptoAnalysisHistoryId.ToString());

        _comparisons.Add(comparison);
        return true;
    }

    public Result<bool, Error> RemoveFromCompareList(Guid cryptoHistoryIdToRemove)
    {
        var comparasionToRemove = _comparisons.FirstOrDefault(x => x.CryptoAnalysisHistoryId == cryptoHistoryIdToRemove);

        if (comparasionToRemove is null)
            return Error.NotFound(nameof(CryptoComparison), nameof(CryptoComparison.CryptoAnalysisHistoryId), cryptoHistoryIdToRemove.ToString());

       return _comparisons.Remove(comparasionToRemove); 
    }

    //helpers
    private bool IsCryptoFavoriteExists(Guid cryptoAnalysisHistoryId) => _favorites.Any(x => x.CryptoAnalysisHistoryId == cryptoAnalysisHistoryId);
    private bool IsCryptoInCompareList(Guid cryptoAnalysisHistoryId) => _comparisons.Any(x => x.CryptoAnalysisHistoryId == cryptoAnalysisHistoryId);

    //validation methods
    private static Result<bool, Error> ValidateName(string name, string propertyName = nameof(FirstName))
    {
        return string.IsNullOrWhiteSpace(name) ? Error.ValueRequired(nameof(User), propertyName) :
               name.Length < 3 || name.Length > 50 ? Error.ValueInvalid(nameof(User), propertyName) :
               true;
    }
    private static Result<bool, Error> ValidateEmail(string email)
    {
        return string.IsNullOrWhiteSpace(email) ? Error.ValueRequired(nameof(User), nameof(Email)) :
            !new EmailAddressAttribute().IsValid(email) ? Error.ValueInvalid(nameof(User), nameof(Email)):
            true;
    }
    private static Result<bool, Error> ValidatePassword(string password)
    {
       return string.IsNullOrWhiteSpace(password) ? Error.ValueRequired(nameof(User), nameof(Password)) :
       password.Length < 5 || password.Length > 20 ? Error.ValueInvalid(nameof(User), nameof(Password)) :
       true;
    }
}