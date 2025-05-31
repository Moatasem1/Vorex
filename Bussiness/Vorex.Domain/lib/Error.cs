namespace Vorex.Domain.lib;

public class Error
{
    public string Code { get; private set; }
    public string Message { get; private set; }
    public string Source { get; private set; }
    public ErrorType ErrorType { get; private set; }

    private Error(ErrorType error ,string code,string source, string message)
    {
        Code = code;
        Message = message;
        ErrorType = error;
        Source = source;
    }

    public static Error ValueRequired(string source,string property) =>
        new (ErrorType.Validation, "VALUE_REQUIRED",source, $"{property} is required.");

    public static Error ValueInvalid(string source, string property) =>
        new (ErrorType.Validation,"VALUE_INVALID",source, $"{property} is invalid.");

    public static Error ValueAlreadyExists(string source, string property,string propertyValue) =>
           new(ErrorType.Conflict, "VALUE_ALREADY_EXISTS", source, $"{property}: '{propertyValue}' already exists.");

    public static Error NotFound(string source, string property, string propertyValue) =>
            new(ErrorType.NotFound, "NOT_FOUND", source, $"{property}: '{propertyValue}' not found.");

    public static Error EmailVerified(string source, string property, string propertyValue) =>
            new(ErrorType.Conflict, "ALREADRY_VERIFIED", source, $"{property}: '{propertyValue}' is already verified please log in");

    public static Error Unauthorized() =>
           new(ErrorType.Unauthorized, "UN_AUTHORIZED", "JwtMiddleware", "You are not authorized to access this resource. Please log in");
    public static Error Forbidden() =>
       new(ErrorType.Forbidden, "FORBIDDEN", "JwtMiddleware", "You do not have permission to access this resource");

}
