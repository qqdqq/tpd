using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TPD.Data;
using TPD.Models;

namespace TPD.Pages.DailyReports
{
    public class CreateModel : PageModel
    {
        private readonly TPD.Data.ApplicationDbContext _context;

        public CreateModel(TPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["WeatherId"] = new SelectList(_context.Weathers, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public DailyReport DailyReport { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.DailyReports.Add(DailyReport);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}