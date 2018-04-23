using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TPD.Data;
using TPD.Models;

namespace TPD.Pages.Attractions
{
    public class DeleteModel : PageModel
    {
        private readonly TPD.Data.ApplicationDbContext _context;

        public DeleteModel(TPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Attraction Attraction { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Attraction = await _context.Attractions
                .Include(a => a.AttractionType)
                .Include(a => a.Locaiton)
                .Include(a => a.ParkInfo).SingleOrDefaultAsync(m => m.Id == id);

            if (Attraction == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Attraction = await _context.Attractions.FindAsync(id);

            if (Attraction != null)
            {
                
                _context.Attractions.Remove(Attraction);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
