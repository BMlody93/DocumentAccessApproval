# DocumentAccessApproval Web API (.NET 8.0)

This is a multi-project solution implementing a RESTful Web API for managing document access approval workflows. It demonstrates layered architecture, separation of concerns, JWT-based authentication, and unit testing using NUnit.

## Project Structure and Key Design Decisions

The solution is organized into five projects for clear separation of concerns:

- DocumentAccessApproval.WebApi  
  Contains API controllers (AuthController, DocumentController, RequestAccessController) and DTO classes. Exposes endpoints.

- DocumentAccessApproval.Domain  
  Holds domain models and interfaces that define contracts for data and business logic.

- DocumentAccessApproval.DataLayer  
  Implements the EF Core DatabaseContext using an in-memory database provider for fast and simple development and testing.

- DocumentAccessApproval.BusinessLogic  
  Contains service implementations of domain interfaces, handling core business operations such as access request creation and decision making.

- DocumentAccessApproval.Test  
  Includes NUnit-based unit tests for logic and data scenarios.

Design follows layered architecture principles, clean code practices, and interface-driven development to facilitate unit testing and maintainability.

## Assumptions and Trade-offs

- An in-memory EF Core database was used to simplify setup and testing. This eliminates the need for SQL setup but lacks persistence across sessions.
- Example users and documents are seeded directly from code instead of using migrations or SQL scripts.
- JWT-based authentication is implemented, but password hashing and user management are simplified for demonstration purposes.
- The focus was on the core access approval flow, with minimal user management.
- Some best practices such as logging, and global exception handling were omitted for brevity.

## How to Run the App and Tests

### Prerequisites

- Visual Studio 2022
- .NET 8.0 SDK

### Installation and Running with Docker

1. Clone the GitHub repository:

   ```bash
   git clone https://github.com/BMlody93/DocumentAccessApproval.git
   cd DocumentAccessApproval

Build and start the Docker containers with Docker Compose:

  docker-compose up --build

This command builds the Docker image(s) from the source code and starts the container(s).

Once started, your API will be available at:

http://localhost:<PORT>

Replace <PORT> with the port specified in your docker-compose.yml file (commonly 5000 or 8080).

Stopping the Application

To stop the containers, press Ctrl+C in the terminal running docker-compose, or run:

  docker-compose down

Running Tests

Tests are part of the solution but not currently included in the Docker image. To run tests locally, use Visual Studio or the .NET CLI:

dotnet test

## Planned Improvements (Given More Time)

If the project were to be extended or productionized, the following enhancements would be prioritized:

- Implement centralized exception handling using middleware and custom exception classes
- Replace in-memory database with a persistent database (e.g., SQLite or SQL Server) using EF migrations
- Hash and salt passwords for secure storage
- Add structured logging using tools such as Serilog
- Improve user management (adding, deleting, editing users) via a dedicated UserController
- Expand unit test coverage to include integration tests and edge cases
- Separate domain models and data entities to support a cleaner architecture
- Containerize the solution using Docker for easier deployment

## GitHub Repository

The solution is available on GitHub at:  
https://github.com/BMlody93/DocumentAccessApproval
