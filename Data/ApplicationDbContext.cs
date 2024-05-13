using Labb4.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

namespace Labb4.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
    }
}
