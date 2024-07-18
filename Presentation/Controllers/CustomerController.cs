using Application.Usecases.Customer.Command.CreateCustomer;
using Application.Usecases.Customer.Query.GetCustomer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Messages;
using Shared.Utils;

namespace Presentation.Controllers;

public class CustomerController(
    IMessageHandlerService errorWarningHandlingService,
    IMediator mediator)
    : ArtesianWellBaseController(errorWarningHandlingService)
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetCustomer([FromQuery] GetCustomerQuery query)
    {
        var result = await mediator.Send(query, CancellationToken.None);
        return HandleResult(result);
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
    {
        var result = await mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }
}