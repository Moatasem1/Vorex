using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vorex.Application.others;
using Vorex.Domain.lib;
using Vorex.Infrastructure.Persistence.Repositories.interfaces;

namespace Vorex.WebApi.Controllers.abstraction;

public abstract class ApiControllerBase() : ControllerBase
{
    
    protected IActionResult HandleResult<T>(Result<T,Error> result)
    {
        if (result.IsSuccess)
            return Ok(ResponseEnvelope<T>.Success(result.Value));

        return GetErorr(result);
    }

    protected IActionResult HandleResult<T,TDto>(Result<T, Error> result, Func<T, TDto> dtoConverterDelegate)
    {
        if (result.IsSuccess)
        {
            var dataAsDto = dtoConverterDelegate(result.Value);
            return Ok(ResponseEnvelope<TDto>.Success(dataAsDto));
        }

        return GetErorr(result);
    }

    protected IActionResult HandleResult<T, TDto>(Result<List<T>, Error> result, Func<T, TDto> dtoConverterDelegate)
    {
        if (result.IsSuccess)
        {
            var dataAsDto = result.Value.Select(v => dtoConverterDelegate(v)).ToList();
            return Ok(ResponseEnvelope<List<TDto>>.Success(dataAsDto));
        }

        return GetErorr(result);
    }

    protected IActionResult HandleResult<T, TDto>(Result<PaginatedResponse<T>, Error> result, Func<T, TDto> dtoConverterDelegate)
    {
        if (result.IsSuccess)
        {
            var dataAsDto = result.Value.Data.Select(v => dtoConverterDelegate(v)).ToList();
            return Ok(ResponseEnvelope<PaginatedResponse<TDto>>.Success(new PaginatedResponse<TDto>(dataAsDto, result.Value.Pagination)));
        }

        return GetErorr(result);
    }

    private IActionResult GetErorr<T>(Result<T, Error> result)
    {
        return result.Error.ErrorType switch
        {
            ErrorType.NotFound => NotFound(ResponseEnvelope<T>.Fail([result.Error])),
            ErrorType.Conflict => Conflict(ResponseEnvelope<T>.Fail([result.Error])),
            ErrorType.Validation => BadRequest(ResponseEnvelope<T>.Fail([result.Error])),
            ErrorType.Unauthorized => Unauthorized(ResponseEnvelope<T>.Fail([result.Error])),
            _ => BadRequest(ResponseEnvelope<T>.Fail([result.Error]))
        };
    }
}       
