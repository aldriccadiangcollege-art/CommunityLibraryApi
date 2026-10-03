using CommunityLibraryAPI.Models.DTOs.Loans;
using CommunityLibraryAPI.Models.Entities;
using CommunityLibraryAPI.Repositories.Interfaces;
using CommunityLibraryAPI.Services.Interfaces;

namespace CommunityLibraryAPI.Services.Implementations;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;

    public LoanService(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<IEnumerable<LoanResponseDto>> GetAllLoansAsync() =>
        await _loanRepository.GetAllAsync();

    public async Task<LoanResponseDto?> GetLoanByIdAsync(int id) =>
        await _loanRepository.GetByIdAsync(id);

    public async Task<IEnumerable<LoanResponseDto>> GetLoansByMemberIdAsync(int memberId) =>
        await _loanRepository.GetByMemberIdAsync(memberId);

    public async Task<ServiceResult<LoanResponseDto>> BorrowBookAsync(BorrowBookDto dto)
    {
        var book = await _loanRepository.GetBookForUpdateAsync(dto.BookId);
        if (book == null)
            return new(ServiceResultType.NotFound, null, "Book not found.");

        var member = await _loanRepository.GetMemberByIdAsync(dto.MemberId);
        if (member == null)
            return new(ServiceResultType.NotFound, null, "Member not found.");

        if (!member.IsActive)
            return new(ServiceResultType.Conflict, null, "Inactive members cannot borrow books.");

        if (book.AvailableCopies <= 0)
            return new(ServiceResultType.Conflict, null, "No available copies for this book.");

        var activeLoans = await _loanRepository.GetActiveLoanCountForMemberAsync(dto.MemberId);
        if (activeLoans >= 3)
            return new(ServiceResultType.Conflict, null, "Member has reached the maximum limit of 3 active loans.");

        var borrowedDate = DateTime.UtcNow;
        var loan = new Loan
        {
            BookId = dto.BookId,
            MemberId = dto.MemberId,
            BorrowedDate = borrowedDate,
            DueDate = borrowedDate.AddDays(7),
            Status = "Borrowed"
        };

        book.AvailableCopies--;

        await _loanRepository.AddAsync(loan);
        await _loanRepository.SaveChangesAsync();

        var responseDto = await _loanRepository.GetByIdAsync(loan.Id);
        return new(ServiceResultType.Success, responseDto, null);
    }

    public async Task<ServiceResult<LoanResponseDto>> ReturnBookAsync(int loanId)
    {
        var loan = await _loanRepository.GetLoanEntityByIdAsync(loanId);
        if (loan == null)
            return new(ServiceResultType.NotFound, null, "Loan record not found.");

        if (loan.Status == "Returned" || loan.ReturnedDate.HasValue)
            return new(ServiceResultType.Conflict, null, "This loan has already been returned.");

        loan.ReturnedDate = DateTime.UtcNow;
        loan.Status = "Returned";
        loan.Book.AvailableCopies++;

        await _loanRepository.SaveChangesAsync();

        var responseDto = await _loanRepository.GetByIdAsync(loan.Id);
        return new(ServiceResultType.Success, responseDto, null);
    }
}