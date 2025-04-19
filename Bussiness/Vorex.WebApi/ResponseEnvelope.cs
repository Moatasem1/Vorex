using System.Text.Json.Serialization;
using Vorex.Domain.lib;

namespace Vorex.WebApi;

public class ResponseEnvelope<T>
{
    public object? ResponseData { get; set; }
    public int ApiVersion { get; set; } = 1;


    public static ResponseEnvelope<T> Success(T data)
    {
        return new ResponseEnvelope<T>
        {
            ResponseData = data,
            ApiVersion = 1
        };
    }

    public static ResponseEnvelope<T> Fail(List<Error> errors)
    {
        return new ResponseEnvelope<T>
        {
            ResponseData = new { Errors = errors },
            ApiVersion = 1
        };
    }
}


