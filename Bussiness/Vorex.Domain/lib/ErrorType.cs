namespace Vorex.Domain.lib;

public enum ErrorType
{
    InternalError = 1,
    Validation,
    NotFound,
    Conflict,
    Unauthorized,
    Forbidden,
}
