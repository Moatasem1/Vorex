using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController(IMediator _mediator) : ApiControllerBase
    {
        
        [HttpGet]
        [ProducesResponseType(typeof(ResponseEnvelope<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseEnvelope<Guid>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsers([FromQuery] BasicLoadOptions loadOptions)
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