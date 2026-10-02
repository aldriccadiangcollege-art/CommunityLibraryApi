using System.ComponentModel.DataAnnotations;

namespace CommunityLibraryAPI.DTOs.Members;

public class UpdateMemberDto
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [RegularExpression("^(Student|Faculty)$")]
    public string MembershipType { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}