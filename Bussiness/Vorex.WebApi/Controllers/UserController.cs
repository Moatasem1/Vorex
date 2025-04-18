using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vorex.Application.Users.Commands;
using Vorex.Application.Users.Contracts.Requests;
using Vorex.Domain.lib;
using Vorex.Domain.User;
using Vorex.Infrastructure.Persistence;
using Vorex.Infrastructure.Persistence.Repositories;
using Vorex.Infrastructure.Persistence.Repositories.interfaces;

namespace Vorex.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController(IUnitOfWork _unitOfWork,IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
           var command = CreatUser.Command.Create(request.FirstName, request.LastName, request.Email, request.Password, request.ProfileImage);

            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            await _unitOfWork.SaveChangesAsync();
            return Ok(result.Value); 
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