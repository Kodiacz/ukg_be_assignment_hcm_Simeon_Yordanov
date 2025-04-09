# HumanCapitalManagement API (Task Project)

This is a simplified ASP.NET Core Web API project built for demonstration or testing purposes. It handles user registration, login, and basic user information using an in-memory database.

## 📦 Technologies Used

- [.NET 8](https://dotnet.microsoft.com/)
- ASP.NET Core Web API
- Entity Framework Core (In-Memory Provider)
- Dependency Injection
- xUnit & Moq for Testing
- JWT (JSON Web Tokens) for Authentication and Authorization

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK 8](https://dotnet.microsoft.com/download)
- Visual Studio 2022 or VS Code

## 🚀 Running the Project

This project uses an **in-memory database** for simplicity, making it ideal for development and testing purposes. You do not need to configure or run an external database.

### 📂 Dummy Data

Inside the `HumanCapitalManagement.Infrastructure` project, there's a `DummyData` directory containing four `.json` files. These files act as predefined datasets used to seed the in-memory database.

### 🌱 Seeding and Persistence Behavior

- **Seeding**: When the application starts, a **Seeder** component loads the data from the `DummyData` directory into the in-memory database.
- **Directory & File Copying**: On first runtime only, if the `DummyData` directory does **not** exist in the output (`bin`) folder, it will be created and the initial `.json` files will be copied there.
- **Persistence**: A **Persister** component simulates saving data. It is triggered **only when the application is closed**, persisting any runtime changes back to the `.json` files.

This design provides a realistic testing experience while keeping things lightweight and file-based.

### ▶️ Steps to Run the Project

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/your-repo-name.git
   
2. Navigate to the API project directory and run the application:
   ```bash   
   cd HumanCapitalManagement.API
   dotnet run
   
3. The application will:

- Check if the DummyData directory exists in the runtime output location.
- If not, create it and copy the initial files.
- Seed the in-memory database with the data.
- Persist any changes back to file on application exit.

## 🏗️ Project Structure (Clean Architecture)

This project follows the **Clean Architecture** principles, which aim to separate concerns, making the application more maintainable and testable. Here's an overview of the structure:

- **API Layer** (`HumanCapitalManagement.API`):
  - The entry point of the application.
  - Contains controllers and the main routing logic for handling HTTP requests.

- **Application Layer** (`HumanCapitalManagement.Application`):
  - Contains business logic and service classes (such as `AuthService`) that handle the application's core operations.
  - Interfaces and DTOs are also defined here.

- **Infrastructure Layer** (`HumanCapitalManagement.Infrastructure`):
  - Contains the implementation of the persistence mechanisms (like `Seeder`, `Persister`) and external services.
  - Implements interfaces defined in the Application layer, such as `IAuthRepository`.

- **Domain Layer** (`HumanCapitalManagement.Domain`):
  - Contains the core domain models (e.g., `ApplicationUser`, `Person`, `Role`. `UserRoles`) and domain logic.
  - This layer should not depend on any other layer and serves as the foundation for the rest of the application.

