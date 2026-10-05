A robust RESTful Web API built with .NET 9 that manages books, library members, and the loan transactions connecting them. This system implements a decoupled Layered Architecture utilizing Controllers, Service layers, and specialized Repository interfaces to enforce complex domain validation rules.

Project Architecture & File Structure

The workspace follows a strict layered pattern ensuring complete separation of concerns and maintaining clean, thin controllers:

Controllers — Presentation Boundary handling external clients. Exposes Data Transfer Objects (DTOs) exclusively to keep underlying entities hidden.
  - BooksController 
  - LoansController 
  - MembersController 
Data — Persistence Infrastructure housing the direct Entity Framework Core Database Context.
  -  LibraryDbContext
Database — Raw Relational SQL Database Management assets targeting Microsoft SQL Server deployment instances.
   -  database-design.sql
   -  database-content.sql
Exceptions — Application Domain Exception Blocks targeting clean failure outputs.
    -  ConflictException
    -  NotFoundException
Middleware — Global HTTP Pipeline Interceptors standardizing request pipeline responses.
       GlobalExceptionMiddleware
   Models — Structural components split into pure schema objects and data transfer parameters.
      DTOs (Data Transfer Objects)
           Books (BookDtos)
           Loans (BorrowBookDto, LoanResponseDto)
           Members (CreateMemberDto, MemberDto, UpdateMemberDto)
      Entities (Relational Schemas)
           Book
           Loan
           Member
Repositories — Data Access Layers encapsulated behind abstraction folders.
       Implementations (BookRepository, LoanRepository, MemberRepository)
       Interfaces (IBookRepository, ILoanRepository, IMemberRepository)
Services — Isolated core logic processing engine layers.
       Implementations (BookService, LoanService, MemberService)
       Interfaces (IBookService, ILoanService)



 Composition Root & Dependency Injection Lifecycle

All components inside the initialization assembly adhere strictly to designated runtime lifetimes within the application container host:

   Database Isolation Context — Registered using targeted database configurations pointing straight to internal application setup connection strings.
   Relational Storage Repositories — Configured under standard Scoped lifetimes to process queries efficiently and drop automatically when an active HTTP request ends.
   Business Transaction Handlers — Bound utilizing Scoped registrations matching the repositories to process rules concurrently without encountering captive system dependencies.



 Relational Database Design & Schema

The relational database properties are initialized using foundational database design scripts incorporating strict check bounds, indices, and structural properties:

Structural Schema Mapping Matrix

1. Books Table (Books)
   Id — Integer, Auto-Incrementing Identity, Primary Key Tracking Identifier.
   Title — Unicode Text Character Field, Required.
   Author — Unicode Text Character Field, Required.
   ISBN — Text Character Field, Required, Unique Key Index Constraint.
   Category — Unicode Text Character Field, Required.
   TotalCopies — Integer Field, Required, Logical Value Boundary Check Constraint (Value must be greater than or equal to zero).
   AvailableCopies — Integer Field, Required, Logical Value Boundary Check Constraint (Value must be greater than or equal to zero).

2. Members Table (Members)
   Id — Integer, Auto-Incrementing Identity, Primary Key Tracking Identifier.
   FullName — Unicode Text Character Field, Required.
   Email — Text Character Field, Required, Unique Key Index Constraint.
   MembershipType — Text Character Field, Required, Domain Check Constraint (Values restricted explicitly to 'student' or 'faculty').
   DateJoined — Date and Time Timestamp Field, Required, Automated Database Default Value Assignment.
   IsActive — Binary Bit Boolean Status Flag, Required, Automated Active Default Value Assignment.

3. Loans Table (Loans)
   Id — Integer, Auto-Incrementing Identity, Primary Key Tracking Identifier.
   BookId — Integer Field, Required, Relational Foreign Key Constraint referencing the Books Identification Index.
   MemberId — Integer Field, Required, Relational Foreign Key Constraint referencing the Members Identification Index.
   BorrowedDate — Date and Time Timestamp Field, Required, Automated Database Default Value Assignment.
   DueDate — Date and Time Timestamp Field, Required.
   ReturnedDate — Date and Time Timestamp Field, Nullable.
   Status — Text Character Field, Required, Domain Check Constraint (Values restricted explicitly to 'borrowed', 'returned', or 'overdue').



 Core Library Transactional Business Rules

The application isolates intricate validation checks outside of the presentation layer and directly into the core service framework:
1.  Inventory Control — Checkout requests evaluate physical catalog states to verify quantity criteria. Transactions programmatically decrement the target item availability count, whereas returns increment the value.
2.  Membership Eligibility — Active user status metrics are inspected upon checkout entry. Accounts logged with inactive state parameters are blocked instantly from starting transactions.
3.  Lending Capacities — Members face volume limits capped at a maximum of three active loan tracking profiles simultaneously.
4.  Automatic Aging Dates — Return deadlines calculate systematically at the time of transaction entry, setting the target date exactly seven days past the baseline borrow date.
5.  Clean Failures — Invalid search indicators throw internal exceptions caught by custom handlers to surface uniform status answers. Infractions against inventory volumes, profile blocks, active limits, or re-submitting a returned loan prompt a structured conflict state return.



 Active API Endpoints Matrix

Book Records Services (/api/books)
   `GET /api/books` — Retracts collection arrays from storage using performance-optimized projection structures.
   `GET /api/books/{id}` — Isolates distinct parameters for a single book model primary tracking key.
   `POST /api/books` — Injects a book definition to storage files and yields standard resource creation locations.
   `PUT /api/books/{id}` — Overwrites and updates historical property metrics on an active entity.
   `DELETE /api/books/{id}` — Safely purges an isolated item index from catalog storage tables.

Membership Services (/api/members)
   `GET /api/members` — Scans profiles across active tracking registers.
   `GET /api/members/{id}` — Parses historical registration records and state parameters for a single user tracking identifier.
   `POST /api/members` — Injects a new profile block into data storage layers using specialized user registration inputs.
   `PUT /api/members/{id}` — Alters personal information configurations using filtered parameter structures.
   `DELETE /api/members/{id}` — Drops accounts or flags structural states to deactivate a user file safely.

Transaction Loan Operations (/api/loans)
   `GET /api/loans` — Retrieves unified logs and projects the structural fields directly to a flattened transfer contract incorporating book title and member name text.
   `GET /api/loans/{id}` — Views isolated parameters for an independent transaction log tracking identifier.
   `POST /api/loans` — Verifies conditions across domain rules and initializes a checkout transaction.
   `POST /api/loans/{id}/return` — Commits a check-in transaction, clears open entries, and drops item pool usage locks.
   `GET /api/members/{id}/loans` — Extracts full lending history linked to a targeted individual.



Environment Execution Blueprint

Prerequisites
 Microsoft .NET 9 SDK Compiler Runtime Environment.
 Local Deployment Instance of Microsoft SQL Server or SQL Server LocalDB.

Execution Order
1. Clone the repository locally using standard source control commands and navigate to the project directory root.
2. Setup the local database instance by sequentially executing the design structure script followed immediately by the content seeding script inside your SQL server management utility suite.
3. Open the global project settings files and verify the server target parameters in the connection string match your local database instance name.
4. Open your command terminal utility or IDE build workspace and execute the project restore commands to retrieve all referenced third-party application modules.
5. Initiate the execution runtime command targeting the main project file wrapper to launch the web host engine container.
6. Open your local browser utility and target the application port suffix designated in your project environment property profile to explore full endpoint parameter specifications using the interactive documentation dashboard UI.


Division of Responsibility Matrix

To satisfy clear execution criteria across the project runtime timeline, engineering tasks were split among developers end-to-end across structural layout domains:

• Member A - Lyniel Ranches(Books Domain Engineer) — Formulated book data shapes, boundary transaction layer contracts, repository implementations, abstraction layers, and matching controller frameworks. Managed primary documentation frameworks and formatting for the system layout configurations.
• Member B - Rico Mendoza(Membership & Defensive Validation Architect) — Developed active user profiles data layouts, granular entry data transfer objects, and domain validation handlers. Crafted target protection structures inside the core runtime middleware layer and decoupled error states layout pipelines.
• Member C - Aldric Cadiang(Loans & Relational Infrastructure Specialist) — Designed the complex cross-resource checking architecture, multi-entity relational mappings, and transaction handlers. Formulated base data container settings, raw schema definition scripts, registration logic hooks inside the application composition root, and interactive documentation pipeline automation.
