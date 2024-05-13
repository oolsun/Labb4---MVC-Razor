using System.ComponentModel.DataAnnotations;

namespace Labb4.Models
{
    public enum CustomerGender
    {
        Male, Female, Other
    }
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 3)]
        [Display(Name = "Customer name")]
        public string CustomerName { get; set; }
        [Required]
        [Display(Name = "E-mail")]
        public string CustomerEmail { get; set; }
        [StringLength(15, MinimumLength = 3)]
        [Display(Name = "Phone")]
        public string CustomerPhone { get; set; }
        [Display(Name = "Gender")]
        public CustomerGender CustomerGender { get; set; }

        public List<Loan>? Loans { get; set; }

    }
}
