using CommunityLibraryAPI.Models.DTOs.Books;
using CommunityLibraryAPI.Models.Entities;
using CommunityLibraryAPI.Repositories.Interfaces;
using CommunityLibraryAPI.Services.Interfaces;

namespace CommunityLibraryAPI.Services.Implementations;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<BookDto>> GetAllAsync()
    {
        var books = await _repository.GetAllAsync();

        return books.Select(MapToDto).ToList();
    }

    public async Task<BookDto?> GetByIdAsync(int id)
    {
        var book = await _repository.GetByIdAsync(id);

        if (book == null)
            return null;

        return MapToDto(book);
    }

    public async Task<BookDto> CreateAsync(CreateBookDto dto)
    {
        var book = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            ISBN = dto.ISBN,
            Category = dto.Category,
            TotalCopies = dto.TotalCopies,
            AvailableCopies = dto.TotalCopies
        };

        await _repository.AddAsync(book);

        return MapToDto(book);
    }

    public async Task<bool> UpdateAsync(
        int id,
        UpdateBookDto dto)
    {
        var book = await _repository.GetByIdAsync(id);

        if (book == null)
            return false;

        int borrowedCopies =
            book.TotalCopies - book.AvailableCopies;

        if (dto.TotalCopies < borrowedCopies)
            return false;

        book.Title = dto.Title;
        book.Author = dto.Author;
        book.ISBN = dto.ISBN;
        book.Category = dto.Category;

        book.TotalCopies = dto.TotalCopies;

        book.AvailableCopies =
            dto.TotalCopies - borrowedCopies;

        await _repository.UpdateAsync(book);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var book = await _repository.GetByIdAsync(id);

        if (book == null)
            return false;

        await _repository.DeleteAsync(book);

        return true;
    }

    private static BookDto MapToDto(Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            Category = book.Category,
            TotalCopies = book.TotalCopies,
            AvailableCopies = book.AvailableCopies
        };
    }
}