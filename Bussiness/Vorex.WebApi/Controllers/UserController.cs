using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vorex.Application;
using Vorex.Application.Users.Commands;
using Vorex.Application.Users.Contracts.Requests;
using Vorex.Application.Users.Queries;
using Vorex.Domain.lib;
using Vorex.Domain.User;
using Vorex.Infrastructure.Persistence;
using Vorex.Infrastructure.Persistence.Repositories;
using Vorex.Infrastructure.Persistence.Repositories.interfaces;
using Vorex.WebApi.Controllers.abstraction;
/*not tested*/
namespace Vorex.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController(IUnitOfWork _unitOfWork,IMediator _mediator) : ApiControllerBase
    {
        
        [HttpPost]
        [ProducesResponseType(typeof(ResponseEnvelope<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseEnvelope<Guid>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseEnvelope<Guid>), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
           var command = Application.Users.Commands.CreateUser.Command.Create(request.FirstName, request.LastName, request.Email, request.Password, request.ProfileImage);

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            await _unitOfWork.SaveChangesAsync();

            return HandleResult(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseEnvelope<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseEnvelope<Guid>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsers([FromQuery] LoadOptions loadOptions)
        {
            var query = GetAllUsers.Query.Create(loadOptions.PageIndex, loadOptions.PageSize);

            var result = await _mediator.Send(query);

            return HandleResult(result, fun=>fun.ToUsersListDto());
        }
    }
}

public class UserByIdSpecification : Specification<User>
{
    public UserByIdSpecification(Guid id)
    {
        Criteria = user => user.Id == id;
    }
}