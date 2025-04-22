using Vorex.Domain.lib;

namespace Vorex.Domain.Cryptos.Specifications;

public class CryptoWithHistoricalDataSpecification : Specification<Crypto>
{
    public CryptoWithHistoricalDataSpecification(Guid cryptoId)
    {
        Criteria = c => c.Id == cryptoId;
        AddInclude(c => c.HistoricalPrices);
    }
}
