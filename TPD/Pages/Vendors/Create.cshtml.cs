using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TPD.Data;
using TPD.Models;

namespace TPD.Pages.Vendors
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
        ViewData["LocationId"] = new SelectList(_context.Locaitons, "Id", "Name");
        ViewData["ParkInfoId"] = new SelectList(_context.ParkInfo, "Id", "Country");
        ViewData["VendorTypeId"] = new SelectList(_context.VendorTypes, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Vendor Vendor { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Vendors.Add(Vendor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}