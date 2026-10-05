using CommunityLibraryAPI.Data;
using CommunityLibraryAPI.Models.Entities;
using CommunityLibraryAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityLibraryAPI.Repositories.Implementations;

public class MemberRepository : IMemberRepository
{
    private readonly LibraryDbContext _context;

    public MemberRepository(LibraryDbContext context) => _context = context;

    public async Task<IEnumerable<Member>> GetAllAsync()
        => await _context.Members.AsNoTracking().ToListAsync();

    public async Task<Member?> GetByIdAsync(int id)
        => await _context.Members.FindAsync(id);

    public async Task AddAsync(Member member)
        => await _context.Members.AddAsync(member);

    public void Update(Member member)
        => _context.Members.Update(member);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}