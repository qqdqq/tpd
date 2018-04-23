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

namespace TPD.Pages.DailyReports
{
    public class EditModel : PageModel
    {
        private readonly TPD.Data.ApplicationDbContext _context;

        public EditModel(TPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DailyReport DailyReport { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DailyReport = await _context.DailyReports
                .Include(d => d.Weather).SingleOrDefaultAsync(m => m.Id == id);

            if (DailyReport == null)
            {
                return NotFound();
            }
           ViewData["WeatherId"] = new SelectList(_context.Weathers, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DailyReport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyReportExists(DailyReport.Id))
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

        private bool DailyReportExists(int id)
        {
            return _context.DailyReports.Any(e => e.Id == id);
        }
    }
}
