# .Net 8 PubSub API with RabbitMQ, Apache Kafka, Postgres, and .Net Aspire

## Technologies used

- .Net 8 WebApi
- .Net 8 Console Apps - RabbitMQ consumers & Kafka Consumers
- .Net Aspire 9
- Entity Framework Core
- Postgres
- RabbitMQ
- Rabbit MQ UI
- Apache Kafka
- Kafka Schema Registry
- Kafka UI
- Confluent Cloud Control Center

## Disclaimer

In a real-world application, we would probably only use RabbitMQ or Kafka, not both. Both are being used for demonstration purposes only. Right now this is a single API, but it will be split up into microservices in future iterations.

Also PlainText is not appropriate for a real-world application. I will be updating this to have actual prod settings in the future.

## How to run

### Run .Net Aspire

`cd` into the Books.AppHost directory and then run the following command. (.Net 8 CLI is required to run this. )

```bash
dotnet run
```

### Access the .Net Aspire Dashboard

Once this is running, go to [https://localhost:17298](https://localhost:17298) in a browser to visit the .Net Aspire dashboard.

### Add appsettings.Development.json file

#### Books.API

In the root of the Books.API project, you will need to create your own `appsettings.Development.json` file with a connection string like below. To get the connection string for your Postgres database, you must run .Net Aspire and then click on Resources => postgres container => View in the .Net Aspire Dashboard. In the window below the resources table, you will be able to get your connection string info. You will need Docker Desktop set up on your machine and running to run this solution.

In the root of the Books.API project, run the following command.

```bash
touch appsettings.Development.json
```

Open the file, copy in the json object below, and update it with your actual connection string for Postgres and RabbitMQ.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "postgres-books": "<connection-string>",
    "rabbitmq": "amqp://<user-name>:<password>@localhost:5672"
  }
}
```

#### RabbitMQ Consumers

You will also have to add an `appsettings.Development.json` file to the root of each RabbitMQ consumer app as well. The file contents should look like this:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "rabbitmq": "amqp://<user-name>:<password>@localhost:5672"
  }
}
```

#### Kafka Common and Consumers

##### Kafka Common

You will also have to add an `appsettings.Development.json` file to the root of the `Books.Kafka.Common` class library. The file contents should look like this:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Kafka": {
    "BootstrapServers": "localhost:9092",
    "SchemaRegistryUrl": "http://localhost:8081"
  }
}
```

##### Kafka Consumers

You will also have to add an `appsettings.Development.json` file to the root of each Kafka consumer app as well. The file contents should look like this:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Kafka": {
    "BootstrapServers": "localhost:9092",
    "SchemaRegistryUrl": "http://localhost:8081",
    "ConsumerGroup": "<resource>-group"
  }
}
```

Once you have your Postgres connection string set, restart the .Net Aspire project, and then the project will work.

### Migrations & Seed Data

If you wish to run the migrations, `cd` into the Books.API directory and run the following command while Books.AppHost is running.

```bash
dotnet ef database update
```
