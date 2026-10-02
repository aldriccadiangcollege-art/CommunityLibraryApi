using CommunityLibrary.API.Data;
using CommunityLibrary.API.Models.Entities;
using CommunityLibrary.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityLibrary.API.Repositories.Implementations;

public class MemberRepository : IMemberRepository
{
    private readonly AppDbContext _context;

    public MemberRepository(AppDbContext context) => _context = context;

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