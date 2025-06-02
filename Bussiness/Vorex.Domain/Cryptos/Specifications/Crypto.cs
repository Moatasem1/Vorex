using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vorex.Domain.lib;

namespace Vorex.Domain.Cryptos.Specifications;

public class CryptoSpecification : Specification<Crypto>
{
    public CryptoSpecification(Guid cryptoId)
    {
        Criteria = c => c.Id == cryptoId;
    }
}