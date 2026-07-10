using MediatR;
using OrderService.Domain.Entities;
using OrderService.Application.Interfaces;
using OrderService.Application.Events; // To use OrderCreatedEvent
using MassTransit; // To use the message publisher

namespace OrderService.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint; // The "megaphone" of MassTransit

    public CreateOrderCommandHandler(IOrderRepository repository, IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.CustomerId, request.TotalAmount);

        // 1. Save in PostgreSQL
        await _repository.AddAsync(order, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        // 2. NEW: Publish the event to RabbitMQ
        var orderCreatedEvent = new OrderCreatedEvent(order.Id, order.TotalAmount);
        await _publishEndpoint.Publish(orderCreatedEvent, cancellationToken);

        return order.Id;
    }
}