using Application.Usecases.Authentication.Command.SignIn;
using Application.Usecases.Authentication.Command.SignUp;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Messages;
using Shared.Utils;

namespace Presentation.Controllers;

public class AuthenticationController: ArtesianWellBaseController
{
    
     public IMessageHandlerService ErrorWarningHandlingService { get; }
     public IMediator Mediator;
    
    public AuthenticationController(IMessageHandlerService errorWarningHandlingService, IMediator mediator) : base(errorWarningHandlingService)
    {
        Mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] SignUpCommand command)
    {
        var result = await Mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] SignInCommand command)
    {
        var result = await Mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }
}