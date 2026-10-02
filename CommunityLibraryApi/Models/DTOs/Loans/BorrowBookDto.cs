using System.ComponentModel.DataAnnotations;

namespace CommunityLibraryAPI.Models.DTOs.Loans
{
    public class BorrowBookDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "BookId must be a valid ID.")]
        public int BookId { get; set; }
       
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "MemberId must be a valid ID.")]
        public int MemberId { get; set; }
    }
}
