using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vorex.Application.Users.Contracts;
using Vorex.Application.Users.Contracts.Requests;
using Vorex.Application.Users.Queries;
using Vorex.Infrastructure.Persistence.Repositories;
using Vorex.Infrastructure.Persistence.Repositories.interfaces;
using Vorex.WebApi.Controllers.abstraction;

namespace Vorex.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CryptoCompareController(IMediator mediator, CurrentUserService currentUserService, IUnitOfWork unitOfWork) : ApiControllerBase
    {
        [HttpPost()]
        [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCryptoAnylsisToCompareList(AddCryptoAnlysisToCompareRequest request)
        {
            var command = Application.Users.Commands.AddCryptoAnylsisToCompare.Command.Create(request.CryptoAnlysisHistoryIds, currentUserService.UserId);

            var result = await mediator.Send(command);

            if (result.IsSuccess)
            {
                await unitOfWork.SaveChangesAsync();
            }

            return HandleResult(result);
        }

        [HttpGet()]
        [ProducesResponseType(typeof(ResponseEnvelope<CryptoAnlysisInComareListDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseEnvelope<CryptoAnlysisInComareListDto>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCompareList()
        {
            var query = GetAllCryptoAnlysisInCompare.Query.Create(currentUserService.UserId);

            var result = await mediator.Send(query);

            return HandleResult(result, fun => fun.ToCryptoAnlysisHistoryListDto());
        }

        [HttpDelete("{cryptoAnlysisHistoryId}")]
        [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveFromCompareList(Guid cryptoAnlysisHistoryId)
        {
            var command = Application.Users.Commands.DeleteCryptoAnlysisComparItem.Command.Create(currentUserService.UserId,cryptoAnlysisHistoryId);

            var result = await mediator.Send(command);

            if (result.IsSuccess)
            {
                await unitOfWork.SaveChangesAsync();
            }

            return HandleResult(result);
        }

    }
}
