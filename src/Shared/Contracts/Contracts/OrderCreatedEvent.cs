namespace Contracts;

public record OrderCreatedEvent(Guid OrderId, decimal TotalAmount);