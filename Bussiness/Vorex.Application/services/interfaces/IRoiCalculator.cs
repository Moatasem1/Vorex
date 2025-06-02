using Vorex.Application.services.Dtos;
using Vorex.Domain.lib;

namespace Vorex.Application.services.interfaces;

public interface IRoiCalculator
{
    Task<Result<RoiCalculatorResponseDto,Error>> CalculateRoi(RoiCalculatorInput input);
}
