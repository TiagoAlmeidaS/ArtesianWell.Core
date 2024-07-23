using Application.Usecases.Service.Command.CreateService;
using Application.Usecases.Service.Command.RemoveService;
using Application.Usecases.Service.Command.UpdateService;
using Application.Usecases.Service.Query.GetAllServices;
using Application.Usecases.Service.Query.GetService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Messages;
using Shared.Utils;

namespace Presentation.Controllers;

public class ServicesController(
    IMessageHandlerService errorWarningHandlingService,
    IMediator mediator)
    : ArtesianWellBaseController(errorWarningHandlingService)
{
    [HttpPost]
    public async Task<IActionResult> CreateService([FromBody] CreateServiceCommand query)
    {
        var result = await mediator.Send(query, CancellationToken.None);
        return HandleResult(result);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteService([FromQuery] RemoveServiceCommand command)
    {
        var result = await mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateService([FromBody] UpdateServiceCommand command)
    {
        var result = await mediator.Send(command, CancellationToken.None);
        return HandleResult(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllServices()
    {
        var result = await mediator.Send(new GetAllServicesCommand(), CancellationToken.None);
        return HandleResult(result);
    }
    
    [HttpGet("{serviceId}")]
    public async Task<IActionResult> GetService(string serviceId)
    {
        var result = await mediator.Send(new GetServiceCommand()
        {
            ServiceId = serviceId
        }, CancellationToken.None);
        return HandleResult(result);
    }
}