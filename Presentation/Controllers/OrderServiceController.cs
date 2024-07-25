using Application.Usecases.OrderService.Command.ChangeStatusOrderService;
using Application.Usecases.OrderService.Command.CreateOrderService;
using Application.Usecases.OrderService.Command.SetServiceScheduleDate;
using Application.Usecases.OrderService.Query.GetOrderService;
using Application.Usecases.OrderService.Query.GetOrderServiceByClient;
using Application.Usecases.Service.Command.CreateService;
using Application.Usecases.Service.Command.RemoveService;
using Application.Usecases.Service.Command.UpdateService;
using Application.Usecases.Service.Query.GetAllServices;
using Application.Usecases.Service.Query.GetService;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Messages;
using Shared.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers;

public class OrderServiceController(
    IMessageHandlerService errorWarningHandlingService,
    IMediator mediator)
    : ArtesianWellBaseController(errorWarningHandlingService)
{
    [HttpPost("change-status")]
    [SwaggerOperation(Summary = "OrderService", Description = "Change the status of OrderService")]
    public async Task<IActionResult> ChangeStatusOrderService([FromBody] ChangeStatusOrderServiceCommand command)
    {
        var result = await mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }
    
    [HttpPut]
    [SwaggerOperation(Summary = "OrderService", Description = "Set the schedule date of OrderService")]
    public async Task<IActionResult> SetServiceScheduleDate([FromBody] SetServiceScheduleDateCommand command)
    {
        var result = await mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }
    
    [HttpPost]
    [SwaggerOperation(Summary = "OrderService", Description = "Create a new OrderService")]
    public async Task<IActionResult> CreateOrderService([FromBody] CreateOrderServiceCommand command)
    {
        var result = await mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }
    
    [HttpGet("{serviceId}")]
    [SwaggerOperation(Summary = "OrderService", Description = "Get OrderService by Id")]
    public async Task<IActionResult> GetOrderService(string serviceId)
    {
        var result = await mediator.Send(new GetOrderServiceQuery()
        {
            OrderId = serviceId
        }, CancellationToken.None);
        return HandleResult(result);
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "OrderService", Description = "Get all OrderService by Client")]
    public async Task<IActionResult> GetOrderServiceByClient([FromQuery] GetOrderServiceByClientQuery query)
    {
        var result = await mediator.Send(query, CancellationToken.None);
        return HandleResult(result);
    }
}