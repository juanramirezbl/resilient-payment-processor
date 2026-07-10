using OrderService.Application.Orders.Commands.CreateOrder;
using OrderService.Application.Interfaces;
using OrderService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1. CRITICAL: We tell .NET to find and activate our "Controllers" folder
builder.Services.AddControllers();

// 2. We register MediatR for CQRS
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));

// 3. We register our simulated database
builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();


var app = builder.Build();

// 4. CRITICAL: We connect the HTTP routes (like /api/orders) to the engine
app.MapControllers();

app.Run();