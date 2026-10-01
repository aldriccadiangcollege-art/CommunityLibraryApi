CREATE DATABASE CommunityLibraryDb;
GO

USE CommunityLibraryDb;
GO

CREATE TABLE Books (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Author NVARCHAR(100) NOT NULL,
    ISBN NVARCHAR(20) NOT NULL UNIQUE,
    Category NVARCHAR(50) NOT NULL,
    TotalCopies INT NOT NULL CHECK (TotalCopies >= 0),
    AvailableCopies INT NOT NULL CHECK (AvailableCopies >= 0)
);

CREATE TABLE Members (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    MembershipType NVARCHAR(20) NOT NULL CHECK (MembershipType IN ('Student', 'Faculty')),
    DateJoined DATETIME2 NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE Loans (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BookId INT NOT NULL,
    MemberId INT NOT NULL,
    BorrowedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    DueDate DATETIME2 NOT NULL,
    ReturnedDate DATETIME2 NULL,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN ('Borrowed', 'Returned', 'Overdue')),
    CONSTRAINT FK_Loans_Books FOREIGN KEY (BookId) REFERENCES Books(Id),
    CONSTRAINT FK_Loans_Members FOREIGN KEY (MemberId) REFERENCES Members(Id)
);