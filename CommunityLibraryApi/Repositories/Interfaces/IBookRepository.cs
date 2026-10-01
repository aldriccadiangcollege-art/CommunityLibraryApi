using CommunityLibraryApi.Models.Entities;

namespace CommunityLibraryApi.Repositories.Interfaces;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();

    Task<Book?> GetByIdAsync(int id);

    Task<Book> AddAsync(Book book);

    Task UpdateAsync(Book book);

    Task DeleteAsync(Book book);
}