using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TPD.Data;
using TPD.Models;

namespace TPD.Pages.Vendors
{
    public class DetailsModel : PageModel
    {
        private readonly TPD.Data.ApplicationDbContext _context;

        public DetailsModel(TPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Vendor Vendor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vendor = await _context.Vendors
                .Include(v => v.Location)
                .Include(v => v.ParkInfo)
                .Include(v => v.VendorType).SingleOrDefaultAsync(m => m.Id == id);

            if (Vendor == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
