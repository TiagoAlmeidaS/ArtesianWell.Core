using Application.Usecases.Budget.Command.ContractBudget;
using Application.Usecases.Budget.Command.CreateBudget;
using Application.Usecases.Budget.Command.RejectBudget;
using Application.Usecases.Budget.Query.GetBudgetsByOrderServiceId;
using Application.Usecases.Budget.Query.GetBudgetsWithPendentsStatus;
using Application.Usecases.OrderService.Command.SetServiceScheduleDate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Messages;
using Shared.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers;

/// <summary>
/// Budget Controller
/// </summary>
/// <param name="errorWarningHandlingService"></param>
/// <param name="mediator"></param>
public class BudgetController(
    IMessageHandlerService errorWarningHandlingService,
    IMediator mediator)
    : ArtesianWellBaseController(errorWarningHandlingService)
{
    /// <summary>
    /// Contract a Budget
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("contract")]
    [SwaggerOperation(Summary = "Budget", Description = "Contract a Budget")]
    public async Task<IActionResult> ContractBudget([FromBody] ContractBudgetCommand command)
    {
        var result = await mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }
    
    /// <summary>
    /// Reject a Budget
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("reject")]
    [SwaggerOperation(Summary = "Budget", Description = "Reject a Budget")]
    public async Task<IActionResult> RejectBudget([FromBody] RejectBudgetCommand command)
    {
        var result = await mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }
    
    /// <summary>
    /// Create a new Budget
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [SwaggerOperation(Summary = "Budget", Description = "Create a new Budget")]
    public async Task<IActionResult> CreateBudget([FromBody] CreateBudgetCommand command)
    {
        var result = await mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }
    
    /// <summary>
    /// Get Budget by Order Service Id
    /// </summary>
    /// <param name="orderServiceId"></param>
    /// <returns></returns>
    [HttpGet("order-service/{orderServiceId}")]
    [SwaggerOperation(Summary = "Budget", Description = "Get Budget by Order Service Id")]
    public async Task<IActionResult> GetBudgetByOrderServiceId(string orderServiceId)
    {
        var result = await mediator.Send(new GetBudgetsByOrderServiceIdQuery()
        {
            OrderServiceId = orderServiceId
        }, CancellationToken.None);
        return HandleResult(result);
    }
    
    /// <summary>
    /// Get all Budgets pendent
    /// </summary>
    /// <returns></returns>
    [HttpGet("pendents")]
    [SwaggerOperation(Summary = "Budget", Description = "Get all Budgets pendent")]
    public async Task<IActionResult> GetBudgetByOrderServiceId()
    {
        var result = await mediator.Send(new GetBudgetsWithPendentsStatusQuery(), CancellationToken.None);
        return HandleResult(result);
    }
}