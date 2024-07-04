using Application.Usecases.Customer.Command.UpdateCustomer;
using Application.Usecases.Customer.Query.GetCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Messages;
using Shared.Utils;

namespace Presentation.Controllers;

public class CustomerController(
    IMessageHandlerService errorWarningHandlingService,
    IMediator mediator)
    : ArtesianWellBaseController(errorWarningHandlingService)
{
    [HttpGet("{customerId}")]
    public async Task<IActionResult> Customer([FromQuery] string customerId)
    {
        var query = new GetCustomerQuery()
        {
            CustomerId = customerId
        };
        
        var result = await mediator.Send(query, CancellationToken.None);
        return HandleResult(result);
    }   
    
    [HttpPut("{customerId}")]
    public async Task<IActionResult> Customer([FromQuery] string customerId, [FromBody] UpdateCustomerCommand command)
    {
        command.CustomerId = customerId;
        
        var result = await mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }  
}