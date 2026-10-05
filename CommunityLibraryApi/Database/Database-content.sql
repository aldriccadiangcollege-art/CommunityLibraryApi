USE CommunityLibraryDb;
GO

INSERT INTO Books (Title, Author, ISBN, Category, TotalCopies, AvailableCopies)
VALUES 
    ('The Pragmatic Programmer', 'Andrew Hunt', '9780201616224', 'Technology', 5, 4),
    ('Clean Code', 'Robert C. Martin', '9780132350884', 'Technology', 3, 3),
    ('Design Patterns', 'Erich Gamma', '9780201633610', 'Technology', 2, 2);

INSERT INTO Members (FullName, Email, MembershipType, DateJoined, IsActive)
VALUES 
    ('Juan Cruz', 'juan.cruz@university.edu.ph', 'Student', GETUTCDATE(), 1),
    ('Maria Santos', 'maria.santos@university.edu.ph', 'Faculty', GETUTCDATE(), 1),
    ('Pedro Penduko', 'pedro.penduko@university.edu.ph', 'Student', GETUTCDATE(), 0);

INSERT INTO Loans (BookId, MemberId, BorrowedDate, DueDate, ReturnedDate, Status)
VALUES 
    (1, 1, GETUTCDATE(), DATEADD(day, 7, GETUTCDATE()), NULL, 'Borrowed'),
    (2, 2, DATEADD(day, -10, GETUTCDATE()), DATEADD(day, -3, GETUTCDATE()), DATEADD(day, -4, GETUTCDATE()), 'Returned');