using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Repositories;

// This class implements the IOrderRepository interface
public class InMemoryOrderRepository : IOrderRepository
{
    // We simulate a database using a simple in-memory list
    private readonly List<Order> _orders = new();

    public Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        _orders.Add(order);
        return Task.CompletedTask; // Since it's in-memory, there's no real "wait" (Task)
    }

    public Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        return Task.FromResult(order);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        // In-memory, there's no need to save transactions, so we do nothing
        return Task.CompletedTask;
    }
}