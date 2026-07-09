using OrderService.Domain.Enums;

namespace OrderService.Domain.Entities;

public class Order
{
    // private set, nobody can see out of this class
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public decimal TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    // Parameterless constructor required by Entity Framework Core when reading from the DB
    private Order() { }

    // Public constructor (or Factory Method) to create new orders
    public Order(Guid customerId, decimal totalAmount)
    {
        if (totalAmount <= 0)
            throw new ArgumentException("The order amount must be greater than zero.");

        Id = Guid.NewGuid(); 
        CustomerId = customerId;
        TotalAmount = totalAmount;
        Status = OrderStatus.Pending; 
        CreatedAt = DateTimeOffset.UtcNow;
    }

    // Domain behaviors: Explicit methods that mutate the state in a controlled way
    public void MarkAsPaid()
    {
        Status = OrderStatus.Paid;
    }

    public void MarkAsFailed()
    {
        Status = OrderStatus.Failed;
    }
}