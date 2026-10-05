namespace CommunityLibraryAPI.Models.DTOs.Loans
{
    public class LoanResponseDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public int MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public DateTime BorrowedDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
