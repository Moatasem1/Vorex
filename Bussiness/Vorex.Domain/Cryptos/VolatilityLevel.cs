using Vorex.Domain.Common;

namespace Vorex.Domain.Cryptos;
public class VolatilityLevel : Enumeration
{
    public VolatilityLevel()
    {
       
    }

    protected VolatilityLevel(int id, string name) : base(id, name)
    {
    }

    public readonly static VolatilityLevel Low = new (0, "low");
    public readonly static VolatilityLevel Medium = new (0, "medium");
    public readonly static VolatilityLevel High = new (0, "heigh");
}
