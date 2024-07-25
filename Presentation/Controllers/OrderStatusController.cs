using Application.Usecases.OrderStatus.Query.GetAllOrderStatus;
using Application.Usecases.OrderStatus.Query.GetOrderStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Messages;
using Shared.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers;

public class OrderStatusController(
    IMessageHandlerService errorWarningHandlingService,
    IMediator mediator)
    : ArtesianWellBaseController(errorWarningHandlingService)
{
    [HttpGet]
    [SwaggerOperation(Summary = "OrderStatus", Description = "Get all OrderStatus")]
    public async Task<IActionResult> GetAllOrderStatus()
    {
        var result = await mediator.Send(new GetAllOrderStatusQuery(), CancellationToken.None);
        return HandleResult(result);
    }
    
    [HttpGet("{orderStatusId}")]
    [SwaggerOperation(Summary = "OrderStatus", Description = "Get OrderStatus by Id")]
    public async Task<IActionResult> GetOrderStatus(string orderStatusId)
    {
        var result = await mediator.Send(new GetOrderStatusQuery()
        {
            OrderStatusId = int.Parse(orderStatusId)
        }, CancellationToken.None);
        return HandleResult(result);
    }
}