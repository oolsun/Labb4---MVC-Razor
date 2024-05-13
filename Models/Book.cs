using System.ComponentModel.DataAnnotations;

namespace Labb4.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        [Display(Name ="Book title")]
        public string BookTitle { get; set; }
        [Display(Name = "Loaned out")]
        public bool IsLoanedOut { get; set; }

        public List<Loan>? Loans { get; set; }
    }
}
