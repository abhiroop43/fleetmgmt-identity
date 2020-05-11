# Introduction 
An Identity Management Project using .NET Core and JWT. Implemented using DDD principles (https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice)


# Project Features

 - User registration
 - Login JWT generation
 - Email Notifications
 - Swagger UI
 - API Gateway using Ocelot
 
# Build and Run

The application uses SQL server. Please configure the connection strings in appsettings.json of API project. The SQL scripts are provided in the Database project. If using Visual Studio, please publish them to the database to setup the application database.

# Planned Features
 - Implement Refresh Token
 - Fluent Validations
 - User Password Reset
