using CommunityLibraryAPI.DTOs.Members;

namespace CommunityLibraryAPI.Services.Interfaces;

public interface IMemberService
{
    Task<IEnumerable<MemberDto>> GetAllMembersAsync();
    Task<MemberDto> GetMemberByIdAsync(int id);
    Task<MemberDto> CreateMemberAsync(CreateMemberDto dto);
    Task UpdateMemberAsync(int id, UpdateMemberDto dto);
    Task DeactivateOrDeleteMemberAsync(int id);
}
