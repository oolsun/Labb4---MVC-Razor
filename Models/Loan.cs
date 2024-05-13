using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb4.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }
        [Display(Name = "Loan date")]
        public DateTime LoanDate { get; set; }
        [Display(Name = "Return date")]
        public DateTime ReturnDate { get; set; }
        [Display(Name = "Returned")]
        public bool IsReturned { get; set; } = false;

        [ForeignKey("Customer")]
        [Display(Name = "Customer")]
        public int FK_CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [ForeignKey("Book")]
        [Display(Name = "Book")]
        public int FK_BookId { get; set; }
        public Book? Book { get; set; }
    }
}
