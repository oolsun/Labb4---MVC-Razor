using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb4.Data;
using Labb4.Models;

namespace Labb4.Controllers
{
    public class LoansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Loans
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Loans.Include(l => l.Book).Include(l => l.Customer);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        public IActionResult Index(int? selectedCustomerId)
        {
            var loans = _context.Loans.Include(l => l.Customer).Include(l => l.Book).ToList();

            // Hämta en lista över alla kunder och lägg till en "All Customers" -alternativ för att ta bort filtret
            var customerList = new SelectList(_context.Customers, "CustomerId", "CustomerName", selectedCustomerId);
            ViewBag.CustomerList = customerList;

            // Om en kund har valts, filtrera lån efter den valda kunden
            if (selectedCustomerId.HasValue)
            {
                loans = loans.Where(l => l.Customer.CustomerId == selectedCustomerId.Value).ToList();
            }

            return View(loans);
        }

        // GET: Loans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Customer)
                .FirstOrDefaultAsync(m => m.LoanId == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // GET: Loans/Create
        public IActionResult Create()
        {
            var availableBooks = _context.Books.Where(b => !b.IsLoanedOut).ToList();

            ViewData["FK_BookId"] = new SelectList(availableBooks, "BookId", "BookTitle");
            ViewData["FK_CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName");

            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoanId,LoanDate,ReturnDate,FK_CustomerId,FK_BookId")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                var selectedBook = await _context.Books.FindAsync(loan.FK_BookId);

                if (selectedBook != null)
                {
                    selectedBook.IsLoanedOut = true;

                    _context.Add(loan);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["FK_CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerName", loan.FK_CustomerId);
            ViewData["FK_BookId"] = new SelectList(_context.Books, "BookId", "BookTitle", loan.FK_BookId);
            return View(loan);
        }

        // GET: Loans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            ViewData["FK_BookId"] = new SelectList(_context.Books, "BookId", "BookTitle", loan.FK_BookId);
            ViewData["FK_CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail", loan.FK_CustomerId);
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoanId,LoanDate,ReturnDate,FK_CustomerId,FK_BookId")] Loan loan)
        {
            if (id != loan.LoanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.LoanId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_BookId"] = new SelectList(_context.Books, "BookId", "BookTitle", loan.FK_BookId);
            ViewData["FK_CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerEmail", loan.FK_CustomerId);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Customer)
                .FirstOrDefaultAsync(m => m.LoanId == id);
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan != null)
            {
                _context.Loans.Remove(loan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.LoanId == id);
        }

        public IActionResult Return()
        {
            var loanedBooks = _context.Loans
                .Include(l => l.Customer)
                .Include(l => l.Book)
                .Where(l => l.Book.IsLoanedOut)
                .ToList();

            return View(loanedBooks);
        }

        [HttpPost]
        public IActionResult MarkAsReturned(int loanId)
        {
            var loan = _context.Loans
                                .Include(l => l.Book) // Se till att ladda Book när du hämtar Loan
                                .FirstOrDefault(l => l.LoanId == loanId);

            if (loan != null && loan.Book != null) // Kontrollera om loan och dess book är inte null
            {
                loan.Book.IsLoanedOut = false;
                loan.IsReturned = true;
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Return));
        }
    }
}
