# KitPraid

KitPraid is a modern, cloud-native reference application built with **.NET 9** and **React**. It demonstrates a microservices-based architecture orchestrated using **.NET Aspire**, featuring a dedicated Identity Server, API Gateway, and domain-specific services.

## 🚀 Project Scope

This project serves as a comprehensive template for building scalable distributed applications. It covers:
*   **Microservices Orchestration**: Managing multiple services, ports, and dependencies using .NET Aspire.
*   **Identity & Access Management**: Centralized authentication using an Identity Server implementation (OAuth2 / OIDC).
*   **API Gateway Pattern**: Routing external traffic to appropriate internal microservices.
*   **Modern Frontend**: A reactive user interface built with React and Vite, integrated with the backend via OIDC.
*   **Clean Architecture**: Separation of concerns with Domain, Application, Infrastructure, and API layers.

## 🛠 Technology Stack

### Backend
*   **Framework**: .NET 9.0 (ASP.NET Core)
*   **Language**: C# 13.0
*   **Orchestration**: .NET Aspire
*   **Authentication**: IdentityServer (OAuth2/OpenID Connect)
*   **Architecture**: Clean Architecture / Microservices

### Frontend
*   **Framework**: React
*   **Build Tool**: Vite
*   **Language**: JavaScript/TypeScript

## 📂 Project Structure

The solution is organized into the following main components:

| Project / Directory | Description |
| :--- | :--- |
| **KitPraid.AppHost** | The .NET Aspire orchestrator project that bootstraps the solution, manages environment variables, and handles service discovery. |
| **KitPraid.Gateway** | The API Gateway entry point, routing requests to backend services. |
| **KitPraid.FrontEnd** | Contains the React SPA (`kitpraid` folder) built with Vite. |
| **IdentityServer** | |
| &nbsp;&nbsp;`*.Api` | The host for the Identity Provider. |
| &nbsp;&nbsp;`*.Domain` | Core domain logic for identity. |
| &nbsp;&nbsp;`*.Infrastructure` | Data access and external concerns for identity. |
| &nbsp;&nbsp;`*.Application` | Application business rules for identity. |
| **ProductService** | |
| &nbsp;&nbsp;`*.Api` | REST API for product management. |
| &nbsp;&nbsp;`*.Domain` | Product domain entities. |
| &nbsp;&nbsp;`*.Infrastructure` | Database context and repositories for products. |
| **KitPraid.Components** | Shared service defaults and Aspire extensions for telemetry and health checks. |

## 🔧 Getting Started

### Prerequisites
*   [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
*   [Node.js](https://nodejs.org/) (for the frontend)
*   Docker Desktop (optional, but recommended for containerization)

### Running the Solution

This project uses **.NET Aspire**, allowing you to run the entire distributed system from a single entry point.

1.  **Clone the repository**:
    ```bash
    git clone <repository-url>
    cd KitPraid
    ```

2.  **Trust the development certificate** (if not already done):
    ```bash
    dotnet dev-certs https --trust
    ```

3.  **Run the AppHost**:
    Navigate to the AppHost directory or run from the root solution:
    ```bash
    dotnet run --project KitPraid.AppHost/KitPraid.AppHost.csproj
    ```

4.  **Access the Dashboard**:
    The console output will provide a URL to the **Aspire Dashboard**. From there, you can view logs, traces, and click links to open the Frontend, IdentityServer, or API Swagger UIs.

## 📚 Documentation

For specific implementation details, refer to the documentation files included in the root:
*   `ASPIRE_DYNAMIC_PORT_SOLUTION.md`: Details on how dynamic ports are handled in orchestration.
*   `IDENTITY_SERVER_FLOW_GUIDE.md`: Explanation of the authentication flows.
*   `OAUTH_REACT_SETUP.md` & `REACT_OAUTH_COMPLETE.md`: Guides on the frontend OIDC integration.
*   `DYNAMIC_PORT_CONFIGURATION.md`: Configuration details for service connectivity.
