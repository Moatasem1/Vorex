using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vorex.Application.CryptoAnalysis.Contract;

public record CryptoAnalysisHistoryDto
{
    public Guid Id { get; init; }
    public required string CryptoName { get; init; }
    public required decimal Amount { get; init; }
    public required int HoldingDays { get; init; }
    public required decimal Risk { get; init; }
    public required DateTime SubmitDate { get; init; }
}
