using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vorex.Application;
using Vorex.Application.Cryptos.Commands;
using Vorex.Application.Cryptos.Contracts;
using Vorex.Application.Cryptos.Contracts.Request;
using Vorex.Application.Cryptos.Queries;
using Vorex.Application.Users.Contracts.Requests;
using Vorex.Domain.Cryptos;
using Vorex.Infrastructure.Persistence.Repositories.interfaces;
using Vorex.WebApi.Controllers.abstraction;

namespace Vorex.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CryptoController(IUnitOfWork _unitOfWork, IMediator _mediator, CurrentUserService _currentUserService) : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ResponseEnvelope<List<CryptoBasicDetailsDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseEnvelope<List<CryptoBasicDetailsDto>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCryptos([FromQuery] BasicLoadOptions loadOptions)
        {
            var query = GetAllCryptos.Query.Create(loadOptions.PageIndex, loadOptions.PageSize,loadOptions.SearchValue);

            var result = await _mediator.Send(query);

            return HandleResult(result, fun => fun.ToCryptoBasicDetailsDto());
        }

        [HttpPost("{cryptoId}/analyze-risk")]
        [ProducesResponseType(typeof(ResponseEnvelope<AnalyzeCryptoRiskResultDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseEnvelope<AnalyzeCryptoRiskResultDto>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AnalyzeRisk(Guid cryptoId, AnalyzeCryptoRiskRequest request)
        {
            var command = AnalyzeCryptoRisk.Command.Create(_currentUserService.UserId, cryptoId,request.InvestmentAmount,request.HoldingDays);

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                await _unitOfWork.SaveChangesAsync();

            return HandleResult(result);
        }

        [HttpGet("{cryptoId}")]
        [ProducesResponseType(typeof(ResponseEnvelope<List<CryptoHistoricalPriceDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseEnvelope<List<CryptoHistoricalPriceDto>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetHistoricalData(Guid cryptoId, [FromQuery] PeriodLoadOptions periodLoadOptions)
        {
            var query = GetCryptoHistoricalData.Query.Create(cryptoId, periodLoadOptions.StartDate,periodLoadOptions.EndDate);

            var result = await _mediator.Send(query);

            return HandleResult(result, fun => fun.ToCryptoHistoricalPriceDto());
        }
    }
}
