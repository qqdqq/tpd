using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TPD.Data;
using TPD.Models;

namespace TPD.Pages.Locations
{
    public class DetailsModel : PageModel
    {
        private readonly TPD.Data.ApplicationDbContext _context;

        public DetailsModel(TPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Location Location { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Location = await _context.Locaitons.SingleOrDefaultAsync(m => m.Id == id);

            if (Location == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
