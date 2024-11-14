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

### Run .Net Aspire

`cd` into the Books.AppHost directory and then run the following command. (.Net 8 CLI is required to run this. )

```bash
dotnet run
```

### Access the .Net Aspire Dashboard

Once this is running, go to [https://localhost:17298](localhost:17298)in a browser to visit the .Net Aspire dashboard.

### Add appsettings.Development.json file

In the root of the Books.API project, you will need to create your own `appsettings.Development.json` file with a connection string like below. To get the connection string for your Postgres database, you must run .Net Aspire and then click on Resources => postgres container => View in the .Net Aspire Dashboard. In the window below the resources table, you will be able to get your connection string info.

In the root of the Books.API project, run the following command.

```bash
touch appsettings.Development.json
```

Open the file, copy in the json object below, and update it with your actual connection string.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "postgres-books": "<connection string>"
  }
}
```

Once you have your connection string set, restart the .Net Aspire project, and then the project will work.

### Seed Data

If you wish to run the migrations, `cd` into the Books.API directory and run the following command while Books.AppHost is running

```bash
dotnet ef database update
```
