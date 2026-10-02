using CommunityLibraryAPI.DTOs.Members;
using CommunityLibraryAPI.Exceptions;
using CommunityLibraryAPI.Models.Entities;
using CommunityLibraryAPI.Repositories.Interfaces;
using CommunityLibraryAPI.Services.Interfaces;

namespace CommunityLibraryAPI.Services.Implementations;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _repository;

    public MemberService(IMemberRepository repository) => _repository = repository;

    public async Task<IEnumerable<MemberDto>> GetAllMembersAsync()
    {
        var members = await _repository.GetAllAsync();
        return members.Select(m => new MemberDto
        {
            Id = m.Id,
            FullName = m.FullName,
            Email = m.Email,
            MembershipType = m.MembershipType,
            DateJoined = m.DateJoined,
            IsActive = m.IsActive
        });
    }

    public async Task<MemberDto> GetMemberByIdAsync(int id)
    {
        var member = await _repository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Member with ID {id} was not found.");

        return new MemberDto
        {
            Id = member.Id,
            FullName = member.FullName,
            Email = member.Email,
            MembershipType = member.MembershipType,
            DateJoined = member.DateJoined,
            IsActive = member.IsActive
        };
    }

    public async Task<MemberDto> CreateMemberAsync(CreateMemberDto dto)
    {
        var member = new Member
        {
            FullName = dto.FullName,
            Email = dto.Email,
            MembershipType = dto.MembershipType,
            DateJoined = DateTime.UtcNow,
            IsActive = true
        };

        await _repository.AddAsync(member);
        await _repository.SaveChangesAsync();

        return new MemberDto
        {
            Id = member.Id,
            FullName = member.FullName,
            Email = member.Email,
            MembershipType = member.MembershipType,
            DateJoined = member.DateJoined,
            IsActive = member.IsActive
        };
    }

    public async Task UpdateMemberAsync(int id, UpdateMemberDto dto)
    {
        var member = await _repository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Member with ID {id} was not found.");

        member.FullName = dto.FullName;
        member.Email = dto.Email;
        member.MembershipType = dto.MembershipType;
        member.IsActive = dto.IsActive;

        _repository.Update(member);
        await _repository.SaveChangesAsync();
    }

    public async Task DeactivateOrDeleteMemberAsync(int id)
    {
        var member = await _repository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Member with ID {id} was not found.");

        member.IsActive = false;
        _repository.Update(member);
        await _repository.SaveChangesAsync();
    }
}
