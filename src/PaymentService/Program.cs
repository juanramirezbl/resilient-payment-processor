using MassTransit;
using PaymentService.Consumers;

var builder = Host.CreateApplicationBuilder(args);

// Configure MassTransit for the worker
builder.Services.AddMassTransit(x =>
{
    // 1. Register our consumer
    x.AddConsumer<OrderCreatedEventConsumer>();

    // 2. Configure the RabbitMQ connection
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h => {
            h.Username("guest");
            h.Password("guest");
        });

        // 3. Configure the receipt endpoint where RabbitMQ will deliver messages
        cfg.ReceiveEndpoint("order-created-queue", e =>
        {
            e.ConfigureConsumer<OrderCreatedEventConsumer>(context);
        });
    });
});
var host = builder.Build();
host.Run();