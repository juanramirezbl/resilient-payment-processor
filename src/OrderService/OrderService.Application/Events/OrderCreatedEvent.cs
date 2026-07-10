namespace OrderService.Application.Events;

// We use record to make it immutable. 
// This is the "package" of information that will travel over the network to other microservices.
public record OrderCreatedEvent(Guid OrderId, decimal TotalAmount);