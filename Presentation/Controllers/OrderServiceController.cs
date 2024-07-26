using Application.Usecases.OrderService.Command.ChangeStatusOrderService;
using Application.Usecases.OrderService.Command.CompletedService;
using Application.Usecases.OrderService.Command.CreateOrderService;
using Application.Usecases.OrderService.Command.ExecutingService;
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

/// <summary>
/// OrderService Controller
/// </summary>
/// <param name="errorWarningHandlingService"></param>
/// <param name="mediator"></param>
public class OrderServiceController(
    IMessageHandlerService errorWarningHandlingService,
    IMediator mediator)
    : ArtesianWellBaseController(errorWarningHandlingService)
{
    /// <summary>
    /// Change the status of OrderService
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("change-status")]
    [SwaggerOperation(Summary = "OrderService", Description = "Change the status of OrderService")]
    public async Task<IActionResult> ChangeStatusOrderService([FromBody] ChangeStatusOrderServiceCommand command)
    {
        var result = await mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }
    
    /// <summary>
    /// Set the schedule date of OrderService
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    [SwaggerOperation(Summary = "OrderService", Description = "Set the schedule date of OrderService")]
    public async Task<IActionResult> SetServiceScheduleDate([FromBody] SetServiceScheduleDateCommand command)
    {
        var result = await mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }
    
    /// <summary>
    /// Create a new OrderService
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [SwaggerOperation(Summary = "OrderService", Description = "Create a new OrderService")]
    public async Task<IActionResult> CreateOrderService([FromBody] CreateOrderServiceCommand command)
    {
        var result = await mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }
    
    /// <summary>
    /// Get all OrderService
    /// </summary>
    /// <param name="serviceId"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// Get all OrderService by Client
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    [SwaggerOperation(Summary = "OrderService", Description = "Get all OrderService by Client")]
    public async Task<IActionResult> GetOrderServiceByClient([FromQuery] GetOrderServiceByClientQuery query)
    {
        var result = await mediator.Send(query, CancellationToken.None);
        return HandleResult(result);
    }
    
    /// <summary>
    /// Conclude a OrderService
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("conclude")]
    [SwaggerOperation(Summary = "OrderService", Description = "Concluded a OrderService")]
    public async Task<IActionResult> CompleteOrderService([FromBody] CompletedServiceCommand command)
    {
        var result = await mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }
    
    /// <summary>
    /// Executing a OrderService
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("executing")]
    [SwaggerOperation(Summary = "OrderService", Description = "Executing a OrderService")]
    public async Task<IActionResult> ExecutingOrderService([FromBody] ExecutingServiceCommand command)
    {
        var result = await mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }
}