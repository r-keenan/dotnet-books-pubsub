# .Net 8 PubSub API with RabbitMQ, Apache Kafka, Postgres, and .Net Aspire

## Technologies used

- .Net 8 WebApi
- .Net Aspire
- Entity Framework Core
- Postgres
- RabbitMQ
- Apache Kafka
- Confluent Cloud Control Center

## Disclaimer

In a real-world application, we would probably only use RabbitMQ or Kafka, not both. Both are being used for demonstration purposes only. Right now this is a single API, but it will be split up into microservices in future iterations.

Also PlainText is not appropriate for a real-world application. I will be updating this to have actual prod settings in the future.

## How to run

`cd` into the Books.AppHost directory and then run the following command. (.Net 8 CLI is required to run this. )

```bash
dotnet run
```
