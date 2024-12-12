using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mesaros_Cristian_Lab2.Data;
using Mesaros_Cristian_Lab2.Models;

namespace Mesaros_Cristian_Lab2.Pages.Borrowings
{
    public class EditModel : PageModel
    {
        private readonly Mesaros_Cristian_Lab2.Data.Mesaros_Cristian_Lab2Context _context;

        public EditModel(Mesaros_Cristian_Lab2.Data.Mesaros_Cristian_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Borrowing = _context.Borrowing
                .Include(b => b.Book)
                .Include(b => b.Member)
                .FirstOrDefault(m => m.ID == id);

            if (Borrowing == null)
            {
                return NotFound();
            }

          
            var bookList = _context.Book
                .Include(b => b.Author)
                .Select(x => new
                {
                    x.ID,
                    BookDetails = x.Title + " - " + x.Author.LastName + " " + x.Author.FirstName
                });

            ViewData["BookID"] = new SelectList(bookList, "ID", "BookDetails");

        
            var memberList = _context.Member
                .Select(x => new
                {
                    x.ID,
                    MemberName = x.FirstName + " " + x.LastName
                });

            ViewData["MemberID"] = new SelectList(memberList, "ID", "MemberName");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Borrowing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowingExists(Borrowing.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BorrowingExists(int id)
        {
            return _context.Borrowing.Any(e => e.ID == id);
        }
    }
}
