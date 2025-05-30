using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vorex.Application.CryptoAnalysis.Contract.Request;

public record DeleteCryptoAnlysisHistoryRecordsRequest
{
    public required List<Guid> Ids { get; init; }
}

public class DeleteCryptoAnlysisHistoryRecordsRequestValidator : AbstractValidator<DeleteCryptoAnlysisHistoryRecordsRequest>
{
    public DeleteCryptoAnlysisHistoryRecordsRequestValidator()
    {
        RuleFor(x=>x.Ids).NotEmpty();

        RuleForEach(x=>x.Ids).NotEmpty();
    }
}