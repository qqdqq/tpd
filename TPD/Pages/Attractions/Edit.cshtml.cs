using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPD.Data;
using TPD.Models;

namespace TPD.Pages.Attractions
{
    public class EditModel : PageModel
    {
        private readonly TPD.Data.ApplicationDbContext _context;

        public EditModel(TPD.Data.ApplicationDbContext context)
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
           ViewData["AttractionTypeId"] = new SelectList(_context.AttractionTypes, "Id", "Name");
           ViewData["LocationId"] = new SelectList(_context.Locaitons, "Id", "Name");
           ViewData["ParkInfoId"] = new SelectList(_context.ParkInfo, "Id", "Country");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Attraction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttractionExists(Attraction.Id))
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

        private bool AttractionExists(int id)
        {
            return _context.Attractions.Any(e => e.Id == id);
        }
    }
}
