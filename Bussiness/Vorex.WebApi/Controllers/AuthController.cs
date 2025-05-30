using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vorex.Application.Users.Contracts;
using Vorex.Application.Users.Contracts.Requests;
using Vorex.Infrastructure.Persistence.Repositories.interfaces;
using Vorex.WebApi.Controllers.abstraction;

namespace Vorex.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IUnitOfWork _unitOfWork, IMediator _mediator) : ApiControllerBase
{
    //change-password

    [HttpPost("register")]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> SingUp(SignUpRequest request)
    {
        var command = Application.Users.Commands.CreateUser.Command.Create(request.FirstName, request.LastName, request.Email, request.Password,null);

        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
        }

        return HandleResult(result);
    }


    [HttpPost("verify-email")]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> VerfiyEmail([FromBody] VerifyEmailRequest request)
    {
        var command = Application.Users.Commands.VerfiyUserEmail.Command.Create(request.Token);

        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
        }

        return HandleResult(result);
    }


    [HttpPost("resend-verify-email")]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> ResendVerfiyEmail(ResendVerifyEmailRequest request)
    {
        var command = Application.Users.Commands.ResendVerifyEmail.Command.Create(request.Email);

        var result = await _mediator.Send(command);

        return HandleResult(result);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseEnvelope<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseEnvelope<LoginResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseEnvelope<LoginResponse>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command = Application.Users.Commands.Login.Command.Create(request.Email,request.Password);

        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
        }

        return HandleResult(result);
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(ResponseEnvelope<RefreshTokenResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseEnvelope<RefreshTokenResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseEnvelope<RefreshTokenResponse>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
    {
        var command = Application.Users.Commands.RefreshToken.Command.Create(request.RefreshToken);

        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
        }

        return HandleResult(result);
    }

    [HttpPost("forgot-password")]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
    {
        var command = Application.Users.Commands.ForgotPassword.Command.Create(request.Email);

        var result = await _mediator.Send(command);

        return HandleResult(result);
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        var command = Application.Users.Commands.ResetPassword.Command.Create(request.Token,request.NewPassword);

        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
        }

        return HandleResult(result);
    }

    [HttpPost("logout")]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseEnvelope<bool>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Logout(LogoutRequest request)
    {
        var command = Application.Users.Commands.Logout.Command.Create(request.RefreshToken);

        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync();
        }

        return HandleResult(result);
    }
}