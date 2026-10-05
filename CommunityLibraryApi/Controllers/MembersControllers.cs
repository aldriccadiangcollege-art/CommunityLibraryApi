using CommunityLibraryAPI.DTOs.Members;
using CommunityLibraryAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommunityLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MembersController(IMemberService memberService) => _memberService = memberService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetAll()
        => Ok(await _memberService.GetAllMembersAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MemberDto>> GetById(int id)
    {
        var member = await _memberService.GetMemberByIdAsync(id);
        if (member == null)
            return NotFound(new { message = "Member not found." });

        return Ok(member);
    }

    [HttpPost]
    public async Task<ActionResult<MemberDto>> Create([FromBody] CreateMemberDto dto)
    {
        var created = await _memberService.CreateMemberAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMemberDto dto)
    {
        await _memberService.UpdateMemberAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _memberService.DeactivateOrDeleteMemberAsync(id);
        return NoContent();
    }
}