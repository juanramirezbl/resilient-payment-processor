using MediatR;

namespace OrderService.Application.Orders.Commands.CreateOrder;

// Usamos 'record' en lugar de 'class' porque los comandos deben ser inmutables (no se pueden modificar en el camino).
// IRequest<Guid> le dice a MediatR que, al terminar este proceso, devolveremos un Guid (el ID de la orden).
public record CreateOrderCommand(Guid CustomerId, decimal TotalAmount) : IRequest<Guid>;