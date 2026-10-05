using CommunityLibraryAPI.Models.DTOs.Books;
using CommunityLibraryAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommunityLibraryAPI.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBookService _service;

    public BooksController(IBookService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<ActionResult<List<BookDto>>> GetAll()
    {
        var books = await _service.GetAllAsync();

        return Ok(books);
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookDto>> GetById(int id)
    {
        var book = await _service.GetByIdAsync(id);

        if (book == null)
            return NotFound();

        return Ok(book);
    }

 
    [HttpPost]
    public async Task<ActionResult<BookDto>> Create(
        CreateBookDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            return BadRequest("Title is required.");

        if (string.IsNullOrWhiteSpace(dto.Author))
            return BadRequest("Author is required.");

        if (string.IsNullOrWhiteSpace(dto.ISBN))
            return BadRequest("ISBN is required.");

        if (string.IsNullOrWhiteSpace(dto.Category))
            return BadRequest("Category is required.");

        if (dto.TotalCopies < 0)
            return BadRequest(
                "TotalCopies cannot be negative.");

        var book = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = book.Id },
            book);
    }

   
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateBookDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            return BadRequest("Title is required.");

        if (string.IsNullOrWhiteSpace(dto.Author))
            return BadRequest("Author is required.");

        if (string.IsNullOrWhiteSpace(dto.ISBN))
            return BadRequest("ISBN is required.");

        if (string.IsNullOrWhiteSpace(dto.Category))
            return BadRequest("Category is required.");

        if (dto.TotalCopies < 0)
            return BadRequest(
                "TotalCopies cannot be negative.");

        var updated = await _service.UpdateAsync(id, dto);

        if (!updated)
            return BadRequest(
                "Book not found or TotalCopies cannot be less than borrowed copies.");

        return NoContent();
    }

  
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}