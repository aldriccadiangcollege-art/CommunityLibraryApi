using CommunityLibraryAPI.Models.DTOs.Loans;
using CommunityLibraryAPI.Models.Entities;

namespace CommunityLibraryAPI.Repositories.Interfaces;

public interface ILoanRepository
{
    Task<IEnumerable<LoanResponseDto>> GetAllAsync();
    Task<LoanResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<LoanResponseDto>> GetByMemberIdAsync(int memberId);
    Task<Book?> GetBookForUpdateAsync(int bookId);
    Task<Member?> GetMemberByIdAsync(int memberId);
    Task<int> GetActiveLoanCountForMemberAsync(int memberId);
    Task<Loan?> GetLoanEntityByIdAsync(int id);
    Task AddAsync(Loan loan);
    Task SaveChangesAsync();



}