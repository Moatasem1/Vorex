using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vorex.Application;
using Vorex.Application.CryptoAnalysis.Commands;
using Vorex.Application.CryptoAnalysis.Contract;
using Vorex.Application.CryptoAnalysis.Contract.Request;
using Vorex.Application.CryptoAnalysis.Queries;
using Vorex.Infrastructure.Persistence.Repositories.interfaces;
using Vorex.WebApi.Controllers.abstraction;

namespace Vorex.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class CryptoAnalysisHistoryController(IUnitOfWork _unitOfWork, IMediator _mediator, CurrentUserService _currentUserService) : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseEnvelope<List<CryptoAnalysisHistoryDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseEnvelope<List<CryptoAnalysisHistoryDto>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCryptosAnlysisHistory([FromQuery] AdvanceLoadOptions loadOptions)
    {
        var query = GetAllCryptoAnylsisHistoryRecords.Query.Create(_currentUserService.UserId, loadOptions.PageIndex, loadOptions.PageSize, loadOptions.SearchValue, loadOptions.StartDate, loadOptions.EndDate);

        var result = await _mediator.Send(query);

        return HandleResult(result, fun => fun.ToCryptoAnalysisHistoryDto());
    }

    [HttpDelete()]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteHistoryRecords(DeleteCryptoAnlysisHistoryRecordsRequest request)
    {
        var command = DeleteCryptoAnalysisHistoryRecords.Command.Create(_currentUserService.UserId,request.Ids);

        var result = await _mediator.Send(command);

        if (result.IsSuccess)
            await _unitOfWork.SaveChangesAsync();

        return HandleResult(result);
    }

    [HttpDelete("all")]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status400BadRequest)]
    /*still we need to remove favourites and comparsion list*/
    public async Task<IActionResult> ClearHistory()
    {
        var command = ClearCryptoAnalysisHistory.Command.Create(_currentUserService.UserId);

        var result = await _mediator.Send(command);

        if (result.IsSuccess)
            await _unitOfWork.SaveChangesAsync();

        return HandleResult(result);
    }
}