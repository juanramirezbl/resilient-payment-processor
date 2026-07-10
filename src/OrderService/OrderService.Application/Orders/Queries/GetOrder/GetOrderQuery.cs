using MediatR;

namespace OrderService.Application.Orders.Queries.GetOrder;

// The flat object we will return to the API
public record OrderResponse(Guid Id, Guid CustomerId, decimal TotalAmount, string Status);

// The Query itself. We tell MediatR that we will return an OrderResponse
public record GetOrderQuery(Guid OrderId) : IRequest<OrderResponse?>;