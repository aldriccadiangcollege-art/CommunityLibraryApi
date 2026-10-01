using System.ComponentModel.DataAnnotations;

namespace CommunityLibrary.API.DTOs.Members;

public class CreateMemberDto
{
    [Required(ErrorMessage = "Full name is required.")]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [RegularExpression("^(Student|Faculty)$", ErrorMessage = "MembershipType must be 'Student' or 'Faculty'.")]
    public string MembershipType { get; set; } = string.Empty;
}