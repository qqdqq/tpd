using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TPD.Data;
using TPD.Models;

namespace TPD.Pages.DailyReports
{
    public class DeleteModel : PageModel
    {
        private readonly TPD.Data.ApplicationDbContext _context;

        public DeleteModel(TPD.Data.ApplicationDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DailyReport = await _context.DailyReports.FindAsync(id);

            if (DailyReport != null)
            {
                _context.DailyReports.Remove(DailyReport);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
