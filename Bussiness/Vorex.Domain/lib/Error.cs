namespace Vorex.Domain.lib;

public class Error
{
    private string _code;
    private string _message { get; }
    private string _source { get; }
    private ErrorType _errorType { get; }

    private Error(ErrorType error ,string code,string source, string message)
    {
        _code = code;
        _message = message;
        _errorType = error;
        _source = source;
    }

    public static Error ValueRequired(string source,string property) =>
        new (ErrorType.BUSINESS_RULE, "VALUE_REQUIRED",source, $"{property} is required.");

    public static Error ValueInvalid(string source, string property) =>
        new (ErrorType.BUSINESS_RULE,"VALUE_INVALID",source, $"{property} is invalid.");

    public static Error ValueAlreadyExists(string source, string property,string propertyValue) =>
           new(ErrorType.BUSINESS_RULE, "VALUE_ALREADY_EXISTS", source, $"{property}: '{propertyValue}' already exists.");

    public static Error NotFound(string source, string property, string propertyValue) =>
            new(ErrorType.BUSINESS_RULE, "NOT_FOUND", source, $"{property}: '{propertyValue}' not found.");
}