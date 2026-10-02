namespace CommunityLibraryApi.Models.Entities;

public class Member
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MembershipType { get; set; } = string.Empty;
    public DateTime DateJoined { get; set; }
    public bool IsActive { get; set; }

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}

