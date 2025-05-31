using System;
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
        IsEmailConfirmed = false;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string? ProfileImage { get; private set; }
    public bool IsEmailConfirmed { get; private set; }

    private readonly List<CryptoFavorite> _favorites = new();
    public virtual IReadOnlyCollection<CryptoFavorite> Favorites => _favorites.AsReadOnly();

    private readonly List<CryptoComparison> _comparisons = new();
    public virtual IReadOnlyCollection<CryptoComparison> Comparisons => _comparisons.AsReadOnly();

    private readonly List<RefreshToken> _refreshTokens = new();
    public virtual IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

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

    public string FullName => $"{FirstName} {LastName}";
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

    public Result<bool, Error> ConfirmEmail()
    {
       IsEmailConfirmed = true;
        return true;
    }

    public Result<bool,Error> ChangePassword(string newHashedPassword)
    {
        Password = newHashedPassword;

        return true;
    }

    public Result<bool, Error> ChangeProfileImage(string? newProfileImage)
    {
        ProfileImage = newProfileImage;
        return true;
    }

    public Result<bool, Error> AddToFavorites(CryptoFavorite favorite)
    {
        if (IsCryptoFavoriteExists(favorite.CryptoId))
            return Error.ValueAlreadyExists(nameof(CryptoFavorite), nameof(CryptoFavorite.CryptoId), favorite.CryptoId.ToString());

        _favorites.Add(favorite);
        return true;
    }

    public Result<bool, Error> RemoveFromFavorites(Guid cryptoHistoryIdToRemove)
    {
        var favoriteToRemove = _favorites.FirstOrDefault(x => x.CryptoId == cryptoHistoryIdToRemove);

        if (favoriteToRemove is null)
            return Error.NotFound(nameof(CryptoFavorite),nameof(CryptoFavorite.CryptoId), cryptoHistoryIdToRemove.ToString());

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

    public Result<bool, Error> RemoveFromCompareList(List<Guid> cryptoHistoryIdsToRemove)
    {
        if (cryptoHistoryIdsToRemove.Count() == 0) return true;

        var removeCount = _comparisons.RemoveAll(x => cryptoHistoryIdsToRemove.Contains(x.CryptoAnalysisHistoryId));

        if (removeCount==0)
            return Error.NotFound(nameof(CryptoComparison), nameof(CryptoComparison.CryptoAnalysisHistoryId),string.Join(",",cryptoHistoryIdsToRemove));

       return true; 
    }

    public Result<bool, Error> AddRefreshToken(RefreshToken refreshToken)
    {
        if (IsRefreshTokenFound(refreshToken.Token))
            return Error.ValueAlreadyExists(nameof(RefreshToken), nameof(RefreshToken.Token), refreshToken.Token);

        _refreshTokens.Add(refreshToken);
        return true;
    }
    public Result<bool, Error> RemoveRefreshToken(string refreshToken)
    {
        var refreshTokenToRemove = _refreshTokens.FirstOrDefault(x => x.Token == refreshToken);
        if (refreshTokenToRemove is null)
            return Error.NotFound(nameof(RefreshToken), nameof(RefreshToken.Token), refreshToken);

        _refreshTokens.Remove(refreshTokenToRemove);
        return true;
    }

    //helpers
    private bool IsCryptoFavoriteExists(Guid cryptoAnalysisHistoryId) => _favorites.Any(x => x.CryptoId == cryptoAnalysisHistoryId);
    private bool IsCryptoInCompareList(Guid cryptoAnalysisHistoryId) => _comparisons.Any(x => x.CryptoAnalysisHistoryId == cryptoAnalysisHistoryId);
    private bool IsRefreshTokenFound(string refreshToken) => _refreshTokens.Any(x => x.Token == refreshToken);

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