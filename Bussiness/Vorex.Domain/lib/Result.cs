using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vorex.Domain.lib.Interfaces;

namespace Vorex.Domain.lib;

public class Result<T,E> : IResult<T,E>
{
    public T Value { get; }
    public bool IsSuccess { get;}
    public bool IsFailure => !IsSuccess;
    public E Error { get;}
    public Result(T value)
    {
        Value = value;
        IsSuccess = true;
        Error = default!;
    }
    public Result(E error)
    {
        Error = error;
        IsSuccess = false;
        Value = default!;
    }

    public static implicit operator Result<T, E>(T value)
    {
        return new Result<T, E>(value);
    }
    public static implicit operator Result<T, E>(E error)
    {
        return new Result<T, E>(error);
    }
}
