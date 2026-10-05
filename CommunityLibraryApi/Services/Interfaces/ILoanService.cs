using CommunityLibraryAPI.Models.DTOs.Loans;

namespace CommunityLibraryAPI.Services.Interfaces;

public enum ServiceResultType
{
    Success,
    NotFound,
    Conflict
}

public record ServiceResult<T>(ServiceResultType ResultType, T? Data, string? ErrorMessage);

public interface ILoanService
{
    Task<IEnumerable<LoanResponseDto>> GetAllLoansAsync();
    Task<LoanResponseDto?> GetLoanByIdAsync(int id);
    Task<IEnumerable<LoanResponseDto>> GetLoansByMemberIdAsync(int memberId);
    Task<ServiceResult<LoanResponseDto>> BorrowBookAsync(BorrowBookDto dto);
    Task<ServiceResult<LoanResponseDto>> ReturnBookAsync(int loanId);
}