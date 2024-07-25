using Application.Usecases.Authentication.Command.SignIn;
using Application.Usecases.Authentication.Command.SignUp;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Messages;
using Shared.Utils;

namespace Presentation.Controllers;

public class AuthenticationController(
    IMessageHandlerService errorWarningHandlingService,
    IMediator mediator)
    : ArtesianWellBaseController(errorWarningHandlingService)
{
    [HttpPost("signup")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] SignUpCommand command)
    {
        var result = await mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }

    [HttpPost("signin")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] SignInCommand command)
    {
        var result = await mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }
}