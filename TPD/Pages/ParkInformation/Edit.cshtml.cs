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

namespace TPD.Pages.ParkInformation
{
    public class EditModel : PageModel
    {
        private readonly TPD.Data.ApplicationDbContext _context;

        public EditModel(TPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ParkInfo ParkInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParkInfo = await _context.ParkInfo.SingleOrDefaultAsync(m => m.Id == id);

            if (ParkInfo == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ParkInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkInfoExists(ParkInfo.Id))
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

        private bool ParkInfoExists(int id)
        {
            return _context.ParkInfo.Any(e => e.Id == id);
        }
    }
}
