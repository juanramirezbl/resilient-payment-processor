using Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace PaymentService.Consumers;

// The IConsumer<T> interface tells MassTransit which specific event to listen for
public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly ILogger<OrderCreatedEventConsumer> _logger;

    public OrderCreatedEventConsumer(ILogger<OrderCreatedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        // 1. Extract the event data received from RabbitMQ
        var orderId = context.Message.OrderId;
        var amount = context.Message.TotalAmount;

        // 2. Simulate payment processing
        _logger.LogInformation("==========================================");
        _logger.LogInformation("💳 EVENT RECEIVED IN PAYMENT SERVICE");
        _logger.LogInformation($"Processing payment for Order: {orderId}");
        _logger.LogInformation($"Amount to charge: {amount} €");
        _logger.LogInformation("✅ Payment processed successfully.");
        _logger.LogInformation("==========================================");

        return Task.CompletedTask;
    }
}