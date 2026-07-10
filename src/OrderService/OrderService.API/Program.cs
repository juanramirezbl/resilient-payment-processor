using OrderService.Application.Orders.Commands.CreateOrder;
using OrderService.Application.Interfaces;
using OrderService.Infrastructure.Persistence.Data; // Required for OrderDbContext
using OrderService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure Controllers
builder.Services.AddControllers();

// 2. Register MediatR for CQRS
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));

// 3. connect PostgreSQL and  repository
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseNpgsql(connectionString));

//  Scoped (real database)
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//  MASSTRANSIT Y RABBITMQ 
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });
    });
});


var app = builder.Build();

// 4. Connect HTTP routes to the engine
app.MapControllers();

app.Run();