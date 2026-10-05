namespace CommunityLibraryAPI.Models.DTOs.Books;

public class BookDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public int TotalCopies { get; set; }

    public int AvailableCopies { get; set; }
}

public class CreateBookDto
{
    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public int TotalCopies { get; set; }
}

public class UpdateBookDto
{
    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public int TotalCopies { get; set; }
}