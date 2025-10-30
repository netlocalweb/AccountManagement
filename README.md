# Account Management System

This is a Web API application for managing client accounts, bank transactions, and financial operations. The system includes user authentication, multi-currency support, and follows clean architecture principles.

## 📋 Table of Contents

- [Project Overview](#project-overview)
- [Architecture & Design Patterns](#architecture--design-patterns)
- [Project Structure](#project-structure)
- [Entity Relationships](#entity-relationships)
- [Class Libraries](#class-libraries)
- [Controllers](#controllers)
- [Repositories](#repositories)
- [Features](#features)
- [Technologies](#technologies)
- [Setup Instructions](#setup-instructions)

## 🎯 Project Overview

The system provides core banking functionality including:
- User registration and authentication
- Client profile management
- Bank account creation and management
- Transaction processing (deposits/withdrawals)
- Financial reporting
- Multi-currency support

Built as a Web API to focus on business logic and enable easy testing through Swagger documentation.

## 🏗️ Architecture & Design Patterns

### Clean Architecture Implementation

The project uses clean architecture with four main layers:
- **Controllers (Presentation)**: API endpoints and request handling
- **Contracts (Application)**: Interface definitions and service contracts
- **Entities (Domain)**: Business models, DTOs, and core domain logic
- **Repository (Infrastructure)**: Data access and Entity Framework implementation

### Design Patterns Used

**1. Repository Pattern**
- Separates data access logic from business logic
- Enables easy database provider switching
- Improves testability through interface abstraction

**2. Unit of Work Pattern**
- Coordinates operations across multiple repositories
- Ensures transaction consistency
- Single save point for all database changes

**3. Dependency Injection**
- Reduces coupling between components
- Improves testability and flexibility
- Built-in ASP.NET Core DI container

**4. DTO Pattern**
- Controls data exposure through API
- Version management for API changes
- Separation between domain models and API contracts

**5. AutoMapper**
- Automatic object-to-object mapping
- Reduces boilerplate code
- Centralized mapping configuration

### Benefits
- **Maintainability**: Clear separation of concerns
- **Testability**: Mockable dependencies and isolated layers
- **Scalability**: Modular structure for easy feature additions
- **Flexibility**: Interface-based design allows implementation changes

## 📁 Project Structure

```
AccountManagement/
├── AccountManagement.sln                 # Solution file
├── README.md                            # Project documentation
│
├── AccountManagement/                   # Main Web API Project
│   ├── Controllers/                     # API Controllers
│   ├── Extensions/                      # Service configuration extensions
│   ├── Migrations/                      # Entity Framework migrations
│   ├── wwwroot/                        # Static files
│   ├── Program.cs                      # Application entry point
│   ├── MappingProfile.cs               # AutoMapper configuration
│   ├── appsettings.json                # Application configuration
│   └── nlog.config                     # Logging configuration
│
├── Contracts/                          # Interface definitions
├── Entities/                           # Domain models and DTOs
├── LoggerService/                      # Logging implementation
└── Repository/                         # Data access layer
```

## 🔗 Entity Relationships

### Core Entities and Their Relationships

```
User (Identity)
    ↓ (1:1)
Client
    ↓ (1:N)
BankAccount
    ↓ (1:N)
BankTransaction

Currency (1:N) → BankAccount
Category (1:N) → Products
```

### Entity Details

#### **User** (Identity Framework)
- **Purpose**: Authentication and authorization
- **Key Properties**: Email, Password, Roles
- **Relationships**: One-to-One with Client

#### **Client**
- **Purpose**: Customer information management
- **Key Properties**: FirstName, LastName, Email, Phone, Birthdate
- **Relationships**: 
  - One-to-One with User
  - One-to-Many with BankAccount

#### **BankAccount**
- **Purpose**: Financial account management
- **Key Properties**: Code, Name, Balance, IsActive
- **Relationships**: 
  - Many-to-One with Client
  - Many-to-One with Currency
  - One-to-Many with BankTransaction

#### **BankTransaction**
- **Purpose**: Financial transaction tracking
- **Key Properties**: Amount, Action (Debit/Credit), TransactionDate
- **Relationships**: Many-to-One with BankAccount

#### **Currency**
- **Purpose**: Multi-currency support
- **Key Properties**: Code, Name, Symbol
- **Relationships**: One-to-Many with BankAccount

#### **Category & Products**
- **Purpose**: Product categorization and management
- **Relationships**: Category has One-to-Many with Products

## 📚 Class Libraries

### 1. **Contracts** Library
**Purpose**: Interface definitions and service contracts

**Key Interfaces**:
- `IRepositoryManager`: Coordinates all repository operations
- `IAuthService`: Authentication and authorization services
- `IClientRepository`, `IBankAccountRepository`, `IBankTransactionRepository`: Data access contracts
- `ILoggerManager`: Logging abstraction

**Benefits**:
- Enables dependency inversion principle
- Improves testability through interface mocking
- Supports loose coupling between layers

### 2. **Entities** Library
**Purpose**: Domain models, DTOs, and core data structures

**Components**:
- `Models/`: Core business entities (Client, BankAccount, BankTransaction, etc.)
- `DTO/`: Data Transfer Objects for API communication
- `Enums/`: System enumerations (TransactionAction, etc.)
- `ErrorModel/`: Custom error handling models

**Benefits**:
- Central location for all data structures
- Shared across multiple projects
- Clean domain modeling with navigation properties

### 3. **LoggerService** Library
**Purpose**: Centralized logging using NLog

**Features**:
- Structured logging with multiple levels
- File and console output
- Configurable for different environments
- Request/response logging

**Benefits**:
- Reusable across projects
- Easy debugging and monitoring
- Centralized logging configuration

### 4. **Repository** Library
**Purpose**: Data access layer implementation

**Components**:
- `RepositoryContext`: Entity Framework DbContext
- `RepositoryBase<T>`: Generic CRUD operations
- Specific repositories for complex business queries
- `RepositoryManager`: Unit of Work implementation

**Benefits**:
- Separation of data access concerns
- Database provider independence
- Testable data layer with mocking support

## 🎮 Controllers

### **AuthenticationController**
**Purpose**: User registration and authentication
- `POST /api/authentication/register` - User registration
- `POST /api/authentication/login` - User login with JWT token generation
- Uses ASP.NET Core Identity for secure password handling

### **ClientController**
**Purpose**: Client profile management
- Full CRUD operations for client information
- Links users to their client profiles
- Handles one-to-one User-Client relationship

### **BankAccountController**
**Purpose**: Bank account management
- Account creation and updates
- Balance inquiries and account details
- Multi-currency support
- Authorization ensures users access only their accounts

### **BankTransactionController**
**Purpose**: Transaction processing
- Deposit and withdrawal operations
- Transaction history retrieval
- Atomic operations for data consistency
- Uses decimal precision for financial calculations

### **CurrencyController**
**Purpose**: Currency management
- CRUD operations for supported currencies
- Currency validation and configuration
- Supports multi-currency banking operations

### **ReportsController**
**Purpose**: Financial reporting and analytics
- Account summaries and balance reports
- Transaction history analysis
- Uses Dapper for optimized complex queries

### **CategoryController & ProductsController**
**Purpose**: Product catalog management
- Category hierarchy management
- Product CRUD operations
- Demonstrates many-to-many relationships

## 🗃️ Repository Implementation

### **Generic Repository Pattern**

#### **RepositoryBase<T>**
**Purpose**: Provides basic CRUD operations for all entity types
- `GetAllAsync()` - Retrieve all records
- `GetByIdAsync(id)` - Find by primary key
- `CreateAsync(entity)` - Add new record
- `UpdateAsync(entity)` - Modify existing record
- `DeleteAsync(entity)` - Remove record
- `FindByConditionAsync(expression)` - Custom LINQ queries

**Benefits**: Code reuse, consistent interface, async operations

### **Specific Repositories**

#### **ClientRepository**
**Purpose**: Client-specific data operations
- `GetClientByUserIdAsync()` - Links users to client profiles
- `GetClientsWithAccountsAsync()` - Includes related account data

#### **BankAccountRepository**
**Purpose**: Account data operations
- `GetAccountsByClientAsync()` - Client's accounts
- `GetAccountWithTransactionsAsync()` - Account with transaction history
- `UpdateBalanceAsync()` - Safe balance updates with validation

#### **BankTransactionRepository**
**Purpose**: Transaction data operations
- `GetTransactionsByAccountAsync()` - Account transaction history
- `GetTransactionHistoryAsync()` - Paginated results
- `CreateTransactionWithBalanceUpdateAsync()` - Atomic transaction creation

#### **CurrencyRepository**
**Purpose**: Currency data operations
- `GetActiveCurrenciesAsync()` - Available currencies
- `GetCurrencyByCodeAsync()` - Find by ISO code

#### **ReportsRepository (Dapper)**
**Purpose**: Complex reporting queries
- Uses Dapper for optimized SQL performance
- Complex joins and aggregations
- Better performance for read-only reporting operations

### **RepositoryManager (Unit of Work)**
**Purpose**: Coordinates operations across repositories

**Features**:
- Lazy initialization of repositories
- Single `SaveAsync()` method for all changes
- Transaction coordination
- Ensures data consistency

**Usage Pattern**:
```csharp
var account = await _repositoryManager.BankAccount.GetByIdAsync(accountId);
var transaction = new BankTransaction { ... };

await _repositoryManager.BankTransaction.CreateAsync(transaction);
await _repositoryManager.BankAccount.UpdateAsync(account);
await _repositoryManager.SaveAsync(); // Atomic operation
```

## ✨ Key Features

### **JWT Authentication**
- Stateless authentication system
- Token-based security without server-side sessions
- Claims-based authorization
- Secure password handling with ASP.NET Core Identity

### **Multi-Currency Support**
- Bank accounts support different currencies (USD, EUR, GBP, etc.)
- Currency validation and management
- Decimal precision for financial calculations
- Extensible currency configuration

### **Transaction Management**
- Debit/Credit operations
- Atomic transaction processing
- Balance validation and updates
- Transaction history tracking
- Data consistency through Unit of Work pattern

### **Comprehensive Logging**
- NLog integration with structured logging
- Multiple log levels (Debug, Info, Warning, Error)
- File and console output
- Environment-specific configuration

### **API Documentation**
- Swagger/OpenAPI integration
- Interactive API testing interface
- Automatic documentation generation
- Authentication testing support

### **Object Mapping**
- AutoMapper for DTO conversions
- Eliminates manual property mapping
- Centralized mapping configuration
- Reduces boilerplate code

### **Database Management**
- Entity Framework Core with Code-First approach
- Database migrations for schema evolution
- Rollback capabilities
- Version control for database structure

### **Error Handling**
- Global exception handling middleware
- Consistent error response format
- Proper error logging
- Security-focused error messages

### **Input Validation**
- Data annotations for declarative validation
- Server-side validation for security
- Custom validation attributes
- Business rule enforcement

## 🛠️ Technologies

- **Framework**: ASP.NET Core 6.0+
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: ASP.NET Core Identity + JWT
- **ORM**: Entity Framework Core + Dapper (for reports)
- **Logging**: NLog
- **Mapping**: AutoMapper
- **Documentation**: Swagger/OpenAPI
- **Architecture**: Clean Architecture with Repository Pattern

## 🚀 Setup Instructions

### Prerequisites
- .NET 6.0 SDK or later
- SQL Server (LocalDB supported)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone Repository**
   ```bash
   git clone [repository-url]
   cd AccountManagement
   ```

2. **Configure Database**
   - Update connection string in `AccountManagement/appsettings.json`
   - LocalDB example:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AccountManagementDB;Trusted_Connection=true"
   }
   ```

3. **Install Dependencies**
   ```bash
   dotnet restore
   ```

4. **Create Database**
   ```bash
   dotnet ef database update --project AccountManagement
   ```

5. **Run Application**
   ```bash
   dotnet run --project AccountManagement
   ```

6. **Access API**
   - API: `https://localhost:7xxx`
   - Swagger UI: `https://localhost:7xxx/swagger`

### Configuration
- **JWT Settings**: Configure token expiration and secret key in `appsettings.json`
- **Logging**: NLog configuration in `nlog.config`
- **CORS**: Modify CORS policy in `ServiceExtensions.cs`

### API Testing
Use Swagger UI to:
1. Register new user
2. Login to obtain JWT token
3. Create client profile
4. Manage bank accounts
5. Process transactions
6. Generate reports

## 📝 API Documentation

Once the application is running, visit `/swagger` for interactive API documentation with all available endpoints, request/response models, and authentication requirements.

## 🔒 Security Features

- JWT token-based authentication
- Role-based authorization
- Password hashing with Identity framework
- Input validation and sanitization
- CORS policy configuration
- Secure API endpoints

## 📈 Project Outcomes

### Technical Achievements
- Implemented clean architecture with proper separation of concerns
- Applied multiple design patterns (Repository, Unit of Work, Dependency Injection)
- Achieved secure authentication with JWT tokens
- Integrated Entity Framework with database migrations
- Implemented comprehensive logging and error handling

### Key Learning Areas
- Clean architecture principles and benefits
- Repository pattern for data access abstraction
- JWT authentication and stateless API design
- Entity Framework Code-First approach
- AutoMapper for object mapping efficiency

### Future Enhancements
- Unit and integration testing implementation
- Performance optimization and caching
- Docker containerization
- CI/CD pipeline setup
- API versioning and rate limiting
- Enhanced security features

---

*Project developed as part of internship program demonstrating enterprise-level .NET development practices.*
