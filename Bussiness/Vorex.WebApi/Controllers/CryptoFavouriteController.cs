using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vorex.Application.Users.Contracts.Requests;
using Vorex.Infrastructure.Persistence.Repositories.interfaces;
using Vorex.WebApi.Controllers.abstraction;

namespace Vorex.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class CryptoFavouriteController(IMediator mediator,IUnitOfWork unitOfWork, CurrentUserService currentUserService) : ApiControllerBase
{
    [HttpPost()]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddCryptoToFavorite(AddCryptoToFavouriteRequest request)
    {
        var command = Application.Users.Commands.AddCryptoToFavourite.Command.Create(request.CryptoId, currentUserService.UserId!.Value);

        var result = await mediator.Send(command);

        if (result.IsSuccess)
            await unitOfWork.SaveChangesAsync();

        return HandleResult(result);
    }

    [HttpDelete("{cryptoId}")]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveCryptoToFavorite(Guid cryptoId)
    {
        var command = Application.Users.Commands.RemoveCryptoFromFavourite.Command.Create(cryptoId, currentUserService.UserId!.Value);

        var result = await mediator.Send(command);

        if (result.IsSuccess)
            await unitOfWork.SaveChangesAsync();

        return HandleResult(result);
    }

    [HttpGet()]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetFavouriteCrypto()
    {
        var command = Application.Users.Queries.GetAllFavouriteCrypto.Query.Create(currentUserService.UserId!.Value);

        var result = await mediator.Send(command);

        return HandleResult(result,fun=>fun.ToFavouriteCryptoDto());
    }
}