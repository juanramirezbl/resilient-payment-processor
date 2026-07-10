using MediatR;
using OrderService.Application.Interfaces;

namespace OrderService.Application.Orders.Queries.GetOrder;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderResponse?>
{
    private readonly IOrderRepository _repository;

    public GetOrderQueryHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<OrderResponse?> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        // 1. Search for the order in the database using the repository
        var order = await _repository.GetByIdAsync(request.OrderId, cancellationToken);

        // 2. If it doesn't exist, return null
        if (order is null) return null;

        // 3. If it exists, map it to our clean and safe DTO
        return new OrderResponse(
            order.Id,
            order.CustomerId,
            order.TotalAmount,
            order.Status.ToString() // Convert the Enum to text
        );
    }
}