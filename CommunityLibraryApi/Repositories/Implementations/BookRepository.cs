using CommunityLibraryAPI.Data;
using CommunityLibraryAPI.Models.Entities;
using CommunityLibraryAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityLibraryAPI.Repositories.Implementations;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;

    public BookRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _context.Books
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Book> AddAsync(Book book)
    {
        _context.Books.Add(book);

        await _context.SaveChangesAsync();

        return book;
    }

    public async Task UpdateAsync(Book book)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Book book)
    {
        _context.Books.Remove(book);

        await _context.SaveChangesAsync();
    }
}