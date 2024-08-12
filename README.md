# API_32593392 

# This project is developed as part of the CMPG 323 curriculum. The project involves creating a .NET Web API to manage telemetry data, with a focus on tracking time and cost savings associated with automation across different projects and clients. The API is secured using JWT-based authentication and is hosted on Azure. Password to be used for database and API authentication is cmpg@323.

Table of Contents
Project Structure
Getting Started
API Documentation
Authentication and Authorization
Using the API
Deployment
References

# Project Structure
Key Components
Controllers: This folder contains the API controllers responsible for handling HTTP requests and returning appropriate responses.
Models: The models represent the data structures used within the application, including telemetry data and user information.
Migrations: This folder stores the Entity Framework migrations that manage database schema changes.
Program.cs & Startup.cs: These files handle the application configuration, including setting up services, middleware, and the request pipeline.
appsettings.json: This file contains configuration settings, such as connection strings and JWT settings.

# Getting Started
Prerequisites
.NET 6.0 SDK
SQL Server (password is cmpg@323)
Visual Studio 2022

# Azure Account 
  resource-Group (nwu_32593392)
  API-service plan(NEWAPISERVICE)
  Server used (project2svr)
  
# Authentication and Authorization
 use the above password to access everything

# API Documentation
The API provides endpoints to manage telemetry data. The Swagger UI can be accessed at https://localhost:5001/swagger to explore the available endpoints.

Key Endpoints
/api/authenticate/login: Authenticates a user and returns a JWT token.
/api/authenticate/register: Registers a new user.
/api/telemetry: CRUD operations for telemetry data.

# Deployment
This application is hosted on Azure. The deployment process includes:

Creating a Resource Group and a SQL Server on Azure.
Publishing the .NET application to an Azure App Service.
Updating the connection strings in the Azure portal to point to the Azure SQL Database.

# References
.NET 6.0 Documentation: https://docs.microsoft.com/en-us/dotnet/
Azure App Service Documentation: https://docs.microsoft.com/en-us/azure/app-service/
Entity Framework Core: https://docs.microsoft.com/en-us/ef/core/
Youtube : https://youtu.be/-kaBMzOPiP0?si=QtBxY6v_4erUb9Is
        : https://youtu.be/murThM9ATJA?si=NJBcFpQ7jxnrOqet-
        : https://youtu.be/tg_0o6U_J-w?si=A1Y6nbivQYw1Asp_
