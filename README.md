# Resilient Payment System

A distributed system based on microservices and an **Event-Driven Architecture**

---

## Microservices

### 1. OrderService (REST API — Publisher)
- Developed following **Clean Architecture** and **CQRS** (Command Query Responsibility Segregation) principles.
- Receives HTTP requests, interacts with a relational database (PostgreSQL), and asynchronously publishes the `OrderCreatedEvent` to the message broker.

### 2. PaymentService (Background Worker — Consumer)
- A background service designed with a flat architecture (KISS principle).
- Subscribed to RabbitMQ. Listens for newly created order events and simulates the payment processing workflow.

### 3. Shared Contracts (Class Library)
- A central project containing the event (message) definitions shared between microservices, ensuring strong typing and preventing direct tight coupling.

---

## Technologies & Tools

| Category | Technology |
|---|---|
| **Framework** | .NET 8 (C# 12) |
| **Message Broker** | RabbitMQ |
| **Messaging Integration** | MassTransit |
| **Architectural Patterns** | Clean Architecture, CQRS (MediatR), Event-Driven Architecture |
| **Database** | PostgreSQL + Entity Framework Core (Code-First Migrations) |
| **API Documentation** | Swagger / OpenAPI |
| **Infrastructure** | Docker & Docker Compose |

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Running Locally

**1. Start the infrastructure (RabbitMQ and PostgreSQL):**

```bash
docker-compose up -d
```

**2. Run the Order Service:**

Open a terminal in the root directory and execute:

```bash
dotnet run --project src/OrderService/OrderService.API --launch-profile http
```

Access Swagger at: `http://localhost:<PORT>/swagger`

**3. Run the Payment Service:**

Open a second terminal in the root directory and execute:

```bash
dotnet run --project src/PaymentService
```

**4. Test the Flow:**

- Use Swagger to send a `POST` request to `/api/orders`.
- Monitor the `PaymentService` terminal to see the background worker consume the event and process the payment instantly.

**5. Resilience Test:**

Stop the `PaymentService`, send several order requests via Swagger, and restart the `PaymentService` to observe the queued messages being processed automatically.

---
