using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vorex.Application.Users.Contracts
{
    public record CryptoAnlysisInComareListDto
    {
        public Guid CryptoAnlysisHistoryId { get; init; }
        public required string CryptoName { get; init; }
        public required decimal InvestAmount { get; init; }
        public required int HoldingDays { get; init; }

        public required decimal ReturnOfInvestment {  get; init; }
        public required decimal Risk { get; init; }
    }
}
