# JWT Authentication API

## Overview
This API provides authentication services using JSON Web Tokens (JWT) for securing endpoints. It consists of three main projects: JwtAuthentication.Api, Authentication.Domain, and JwtAuthentication.Infrastructure. The API is built using .NET 8 and ASP.NET Core Web API.

## Project Structure

### JwtAuthentication.Api
This project contains the entry point of the API and is responsible for handling HTTP requests, routing them to the appropriate controllers, and returning responses. It interfaces with the Authentication.Domain and JwtAuthentication.Infrastructure projects to perform authentication and authorization tasks.

### Authentication.Domain
The Authentication.Domain project contains domain-specific logic and entities related to authentication, such as user authentication and authorization rules. It defines interfaces and contracts that are implemented by the JwtAuthentication.Infrastructure project.

### JwtAuthentication.Infrastructure
This project provides concrete implementations for the interfaces defined in the Authentication.Domain project. It includes components responsible for user validation, JWT generation, and other infrastructure-related tasks. Notably, it utilizes a CustomAuthenticationService and UserRepository for user validation.

## Additional Components

### CustomAuthenticationService
This service handles user authentication logic, including verifying user credentials and generating JWT tokens upon successful authentication. It interfaces with the UserRepository to validate user credentials.

### UserRepository
The UserRepository is responsible for accessing and managing user data for authentication purposes. It provides methods for querying user information and validating user credentials.

### GeoUtil and PreconfiguredLocations
These components are used for validating user locations. GeoUtil provides utility functions for working with geographical data, while PreconfiguredLocations contains predefined locations against which user locations can be validated.

## Setup and Configuration

### Prerequisites
- .NET 8 SDK
- ASP.NET Core runtime

### Installation
1. Clone the repository: `git clone https://github.com/your/repository.git`
2. Navigate to the solution directory: `cd JWTAuthenticationAPI`
3. Build the solution: `dotnet build`

### Configuration
1. Configure database connection strings, JWT secret key, and other settings in the `appsettings.json` files of JwtAuthentication.Api and JwtAuthentication.Infrastructure projects.
2. Ensure that necessary dependencies are injected into the IoC container in the Startup class of JwtAuthentication.Api.

### Running the API
1. Navigate to the JwtAuthentication.Api project directory.
2. Run the API: `dotnet run`

## Usage
1. Send HTTP requests to the appropriate endpoints for user authentication and authorization.
2. Include JWT tokens in the Authorization header of subsequent requests to access protected endpoints.

![image](https://github.com/SuryaMasab/JwtAuthentication.Api/assets/114293640/41a7d914-b632-4ae8-b0b5-c1eb1fe6cfba)

![image](https://github.com/SuryaMasab/JwtAuthentication.Api/assets/114293640/c40ef318-03e5-4c60-a35b-df885343a828)
