using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Orders.Commands.CreateOrder;
using OrderService.Application.Orders.Queries.GetOrder;

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
    public async Task<IActionResult> GetOrder(Guid id)
    {
        // Create the query with the ID from the URL
        var query = new GetOrderQuery(id);
        
        // MediatR sends it to the GetOrderQueryHandler
        var order = await _mediator.Send(query);

        // If it's null, we return a standard 404 Not Found
        if (order is null) return NotFound();

        // If it exists, we return it with a 200 OK
        return Ok(order);
    }
}