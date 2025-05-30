using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vorex.Application.Users.Contracts.Requests;

public record VerifyEmailRequest
{
    public required string Token { get; init; }
}
