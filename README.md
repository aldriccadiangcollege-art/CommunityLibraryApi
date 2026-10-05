https://docs.google.com/document/d/1EFZupaElPsMNivHHJbDubNXfHsHqf9jOc1sDNQQ0TuI/edit?usp=sharing

Welcome to our Readme Project!

We are showcasing A robust RESTful Web API built with .NET 9 that manages books, library members, and loan transactions. The application follows a clean Layered Architecture using Controllers, Services, and Repositories, with Entity Framework Core and SQL Server for data persistence.

The API enforces business rules involving inventory availability, member status, lending limits, loan due dates, and transaction validation.

 Project Architecture & File Structure

The project follows a Layered Architecture that separates presentation, business logic, data access, and database responsibilities. This keeps the controllers lightweight and makes the application easier to maintain and test.

- Controllers — Handle incoming HTTP requests and return responses through DTOs.
  - `BooksController`
  - `LoansController`
  - `MembersController`

- Data — Contains the Entity Framework Core database context used to communicate with SQL Server.
  - `LibraryDbContext`

- Database — Contains the SQL scripts used to create and populate the database.
  - `database-design.sql`
  - `database-content.sql`

- Exceptions — Custom domain exceptions targeting clean error handling:
  - `ConflictException` — Maps to HTTP 409 Conflict
  - `NotFoundException` — Maps to HTTP 404 Not Found

- Middleware — Global HTTP pipeline interceptor that standardizes error responses:
  - `GlobalExceptionMiddleware`

- Models — Domain components split into relational entities and Data Transfer Objects.

  - DTOs (Data Transfer Objects)
    - Books
      - `BookDto`
      - `CreateBookDto`
      - `UpdateBookDto`
    - Loans
      - `BorrowBookDto`
      - `LoanResponseDto`
    - Members
      - `CreateMemberDto`
      - `MemberDto`
      - `UpdateMemberDto`

  - Entities
    - `Book`
    - `Loan`
    - `Member`

- Repositories — Data Access Layer encapsulated behind abstraction interfaces.

  - Implementations
    - `BookRepository`
    - `LoanRepository`
    - `MemberRepository`

  - Interfaces
    - `IBookRepository`
    - `ILoanRepository`
    - `IMemberRepository`

- Services — Core business logic processing layer.

  - Implementations
    - `BookService`
    - `LoanService`
    - `MemberService`

  - Interfaces
    - `IBookService`
    - `ILoanService`
    - `IMemberInterfaces’
---
 NuGet Packages

The project uses the following NuGet packages:

- `Microsoft.AspNetCore.OpenApi (9.0.20)`
- `Microsoft.EntityFrameworkCore.SqlServer (9.0.0)`
- `Microsoft.EntityFrameworkCore.Tools (9.0.0)`
- `Swashbuckle.AspNetCore (9.0.6)`
- `Swashbuckle.AspNetCore.SwaggerUI (9.0.6)`

---
 Setup & Execution Instructions

Prerequisites

- .NET 9 SDK
- A local SQL Server instance

The project uses SQL Server as its database provider through Entity Framework Core.

---

 1. Database Creation

The database is managed using raw SQL scripts located in the `/database` directory.

Do not run Entity Framework Core migrations.

Create and populate the database using the following execution order:

1. Connect to your local SQL Server instance using your preferred SQL client.
2. Open and execute:

/database/database-design.sql
This creates the CommunityLibraryDb database, table schemas, primary keys, foreign keys, and domain CHECK constraints.
Open and execute:
/database/database-content.sql
This populates the database with initial seed data for books, members, and loan records.
Important: Run database-design.sql first, followed by database-content.sql.

 2. Connection String Configuration
Update the DefaultConnection string inside appsettings.json or appsettings.Development.json to match your local SQL Server instance.
Example for SQL Server LocalDB:
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CommunityLibraryDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
If you are using a different SQL Server instance, replace the Server value with your own SQL Server name.

 3. Running the Application
Open a terminal in the project directory and run:
dotnet restore
dotnet run
After the application starts, the terminal will display the local URL where the API is running.

 4. Swagger / OpenAPI
The application includes Swagger/OpenAPI documentation for viewing and testing the available API endpoints.
After starting the application, open:
https://localhost:<port>/swagger
Swagger provides an interactive interface for testing the Books, Members, and Loans endpoints.

 Layer Structure & Dependency Injection
The application follows this general request and data flow:
Client
   ↓
Controllers
   ↓
Services
   ↓
Repositories
   ↓
Entity Framework Core
   ↓
SQL Server
DTOs are used at the API boundary so that database entities are not directly exposed to external clients.
The application uses Scoped Dependency Injection lifetimes for the database context, repositories, and services. A scoped instance is created for each HTTP request and is disposed of when the request is completed.
This allows services and repositories to share the same database context during a request while maintaining separation of responsibilities.

 Error Handling
The application uses custom exceptions together with GlobalExceptionMiddleware to provide consistent HTTP error responses.
Custom Exceptions
NotFoundException → HTTP 404 Not Found
ConflictException → HTTP 409 Conflict
GlobalExceptionMiddleware catches exceptions thrown by the application and converts them into appropriate HTTP responses. This keeps error-handling logic out of the controllers and provides consistent responses across the API.

 HTTP Status Codes
200 OK — Request completed successfully.
201 Created — A new resource was successfully created.
204 No Content — Resource was successfully deleted or deactivated with no response body.
400 Bad Request — The request contains invalid or unacceptable data.
404 Not Found — The requested book, member, or loan does not exist.
409 Conflict — The request conflicts with a business rule, such as borrowing an unavailable book, exceeding the three-loan limit, or attempting to return an already returned loan.

 Core Library Business Rules
The application enforces the following business rules through the Service layer:
Inventory Control — A book must have available copies before it can be borrowed.
Membership Eligibility — Only active members can borrow books.
Lending Capacity — A member can have a maximum of three active loans at the same time.
Automatic Due Date — A new loan has a due date set seven days after the borrowing date.
Book Availability — Borrowing decreases the number of available copies, while returning a book increases it.
Loan Validation — A returned loan cannot be returned again.
Clean Error Handling — Invalid resources and business rule conflicts are handled through custom exceptions and global middleware.

 Database Design
The database contains three main tables:
1. Books
The Books table stores information about library books.
Id — Primary key
Title — Required book title
Author — Required author name
ISBN — Required unique ISBN
Category — Required book category
TotalCopies — Total number of copies
AvailableCopies — Number of currently available copies
2. Members
The Members table stores library member information.
Id — Primary key
FullName — Required member name
Email — Required unique email
MembershipType — Student or faculty
DateJoined — Date the member joined
IsActive — Indicates whether the member is active
3. Loans
The Loans table stores borrowing and returning transactions.
Id — Primary key
BookId — Foreign key referencing Books
MemberId — Foreign key referencing Members
BorrowedDate — Date the book was borrowed
DueDate — Required return deadline
ReturnedDate — Date the book was returned, if applicable
Status — Borrowed, returned, or overdue

 API Endpoints
 Books — /api/books
GET /api/books — Retrieve a list of all books
Expected: 200 OK
GET /api/books/{id} — Retrieve details for a specific book
Expected: 200 OK, 404 Not Found
POST /api/books — Add a new book to the catalog
Expected: 201 Created with Location header
PUT /api/books/{id} — Update an existing book record
Expected: 200 OK, 400 Bad Request, 404 Not Found
DELETE /api/books/{id} — Remove a book from catalog storage
Expected: 204 No Content, 404 Not Found
 Members — /api/members
GET /api/members — Retrieve a list of all member profiles
Expected: 200 OK
GET /api/members/{id} — Retrieve details for a specific member
Expected: 200 OK, 404 Not Found
POST /api/members — Register a new library member
Expected: 201 Created, 400 Bad Request
PUT /api/members/{id} — Update member profile information
Expected: 200 OK, 400 Bad Request, 404 Not Found
DELETE /api/members/{id} — Deactivate or remove a member account
Expected: 204 No Content, 404 Not Found
 Loans — /api/loans
GET /api/loans — List all loans, projected with Book Title and Member Name
Expected: 200 OK
GET /api/loans/{id} — Retrieve details for a single loan
Expected: 200 OK, 404 Not Found
POST /api/loans — Borrow a book and verify stock, member status, and the three-loan limit
Expected: 201 Created, 404 Not Found, 409 Conflict
POST /api/loans/{id}/return — Return a borrowed book, increment stock, and block double returns
Expected: 200 OK, 404 Not Found, 409 Conflict
GET /api/members/{id}/loans — List all active and past loans for a specific member
Expected: 200 OK, 404 Not Found

 Team & Contributions
Team & Contributions

- Lyniel Ranches  — Books: Book entity + mapping, DTOs, repository, service, controller. Also owns the README and endpoint documentation.

- Rico Mendoza  — Members: Member entity + mapping, DTOs, repository, service, controller. Also owns validation and the shared error-handling approach.

- Aldric Cadiang  — Loans: Loan entity + relationships, DTOs, repository, service, controller plus the cross-resource business rules. Also owns the `LibraryDbContext`, the database design & content scripts, DI wiring in `Program.cs`, and Swagger setup.

Integration work (the Loan rules touching Books and Members) is shared and should appear as pull requests reviewed by teammates.

Integration Work
Integration work involving Loan rules that interact with Books and Members is shared among the team and should appear as pull requests reviewed by teammates.

 Deliverables
The project includes:
Complete .NET 9 RESTful Web API
SQL Server database scripts
/database/database-design.sql
/database/database-content.sql
Layered architecture using Controllers, Services, and Repositories
Entity Framework Core integration
DTO-based API responses
Custom exception handling
Global exception middleware
Swagger/OpenAPI documentation
Books, Members, and Loans endpoints
Database seed data
Dependency Injection with Scoped lifetimes
Team-based GitHub development and reviewed integration work

