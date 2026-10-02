using CommunityLibraryAPI.Models.Entities;

namespace CommunityLibraryAPI.Repositories.Interfaces;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllAsync();
    Task<Member?> GetByIdAsync(int id);
    Task AddAsync(Member member);
    void Update(Member member);
    Task SaveChangesAsync();
}
