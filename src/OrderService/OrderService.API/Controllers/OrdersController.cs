using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Orders.Commands.CreateOrder;

namespace OrderService.API.Controllers;

[ApiController]
[Route("api/[controller]")] // The URL for this endpoint will be: api/orders
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        // MediatR dispatches the command to the handler
        var orderId = await _mediator.Send(command);

        // Returns HTTP 201 (Created) and the URL to query the order
        return CreatedAtAction(nameof(GetOrder), new { id = orderId }, new { OrderId = orderId });
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetOrder(Guid id)
    {
        // Temporary endpoint for CreatedAtAction to work
        return Ok(new { Id = id, Status = "Pending" });
    }
}