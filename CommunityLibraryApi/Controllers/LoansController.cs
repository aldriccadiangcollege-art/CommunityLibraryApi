using CommunityLibraryAPI.Models.DTOs.Loans;
using CommunityLibraryAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommunityLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var loans = await _loanService.GetAllLoansAsync();
        return Ok(loans);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var loan = await _loanService.GetLoanByIdAsync(id);
        if (loan == null) return NotFound(new { message = "Loan not found." });
        return Ok(loan);
    }

    [HttpGet("/api/members/{memberId:int}/loans")]
    public async Task<IActionResult> GetByMemberId(int memberId)
    {
        var loans = await _loanService.GetLoansByMemberIdAsync(memberId);
        return Ok(loans);
    }

    [HttpPost]
    public async Task<IActionResult> BorrowBook([FromBody] BorrowBookDto dto)
    {
        var result = await _loanService.BorrowBookAsync(dto);

        return result.ResultType switch
        {
            ServiceResultType.Success => CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data),
            ServiceResultType.NotFound => NotFound(new { message = result.ErrorMessage }),
            ServiceResultType.Conflict => Conflict(new { message = result.ErrorMessage }),
            _ => BadRequest()
        };
    }

    [HttpPost("{id:int}/return")]
    public async Task<IActionResult> ReturnBook(int id)
    {
        var result = await _loanService.ReturnBookAsync(id);

        return result.ResultType switch
        {
            ServiceResultType.Success => Ok(result.Data),
            ServiceResultType.NotFound => NotFound(new { message = result.ErrorMessage }),
            ServiceResultType.Conflict => Conflict(new { message = result.ErrorMessage }),
            _ => BadRequest()
        };
    }
}