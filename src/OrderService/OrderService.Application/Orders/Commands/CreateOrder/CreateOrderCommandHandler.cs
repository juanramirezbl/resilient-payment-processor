using MediatR;
using OrderService.Domain.Entities;
using OrderService.Application.Interfaces;

namespace OrderService.Application.Orders.Commands.CreateOrder;

// IRequestHandler tells MediatR what to do
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _repository;

    // Dependency Injection: We ask .NET for a class that implements the IOrderRepository 
    // interface, regardless of whether it's SQL, Mongo, or in-memory.
    public CreateOrderCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // 1. Instantiate the domain entity (it validates its own rules)
        var order = new Order(request.CustomerId, request.TotalAmount);

        // 2. Save the order using the interface
        await _repository.AddAsync(order, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        // 3. Return the ID of the newly created order
        return order.Id;
    }
}