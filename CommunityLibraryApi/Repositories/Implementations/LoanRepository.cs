using CommunityLibraryAPI.Data;
using CommunityLibraryAPI.Models.DTOs.Loans;
using CommunityLibraryAPI.Models.Entities;
using CommunityLibraryAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityLibraryAPI.Repositories.Implementations;

public class LoanRepository : ILoanRepository
{
    private readonly LibraryDbContext _context;

    public LoanRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LoanResponseDto>> GetAllAsync()
    {
        return await _context.Loans
            .AsNoTracking()
            .Select(l => new LoanResponseDto
            {
                Id = l.Id,
                BookId = l.BookId,
                BookTitle = l.Book.Title,
                MemberId = l.MemberId,
                MemberName = l.Member.FullName,
                BorrowedDate = l.BorrowedDate,
                DueDate = l.DueDate,
                ReturnedDate = l.ReturnedDate,
                Status = l.Status
            })
            .ToListAsync();
    }

    public async Task<LoanResponseDto?> GetByIdAsync(int id)
    {
        return await _context.Loans
            .AsNoTracking()
            .Where(l => l.Id == id)
            .Select(l => new LoanResponseDto
            {
                Id = l.Id,
                BookId = l.BookId,
                BookTitle = l.Book.Title,
                MemberId = l.MemberId,
                MemberName = l.Member.FullName,
                BorrowedDate = l.BorrowedDate,
                DueDate = l.DueDate,
                ReturnedDate = l.ReturnedDate,
                Status = l.Status
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<LoanResponseDto>> GetByMemberIdAsync(int memberId)
    {
        return await _context.Loans
            .AsNoTracking()
            .Where(l => l.MemberId == memberId)
            .Select(l => new LoanResponseDto
            {
                Id = l.Id,
                BookId = l.BookId,
                BookTitle = l.Book.Title,
                MemberId = l.MemberId,
                MemberName = l.Member.FullName,
                BorrowedDate = l.BorrowedDate,
                DueDate = l.DueDate,
                ReturnedDate = l.ReturnedDate,
                Status = l.Status
            })
            .ToListAsync();
    }

    public async Task<Book?> GetBookForUpdateAsync(int bookId)
    {
        return await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
    }

    public async Task<Member?> GetMemberByIdAsync(int memberId)
    {
        return await _context.Members.AsNoTracking().FirstOrDefaultAsync(m => m.Id == memberId);
    }

    public async Task<int> GetActiveLoanCountForMemberAsync(int memberId)
    {
        return await _context.Loans
            .AsNoTracking()
            .CountAsync(l => l.MemberId == memberId && l.ReturnedDate == null);
    }

    public async Task<Loan?> GetLoanEntityByIdAsync(int id)
    {
        return await _context.Loans
            .Include(l => l.Book)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task AddAsync(Loan loan)
    {
        await _context.Loans.AddAsync(loan);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}