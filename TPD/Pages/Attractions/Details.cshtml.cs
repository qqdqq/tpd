using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TPD.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using TPD.Authorization;
using TPD.Models;

namespace TPD.Pages.Attractions
{
    public class DetailsModel : DI_BasePageModel
    {
        private readonly TPD.Data.ApplicationDbContext _context;

        public DetailsModel(
           ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager) : base(context, authorizationService, userManager)
        {
            _context = context;
        }

        public Attraction Attraction { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isManager = User.IsInRole(Constants.AttractionManagerRole);
            var isAdmin = User.IsInRole(Constants.AttractionAdministratorsRole);
            if (isAdmin || isManager)
            {
                Attraction = await _context.Attractions
                .Include(a => a.ParkInfo)
                .Include(a => a.AttractionType)
                .Include(a => a.Locaiton)
                .Include(a => a.MaintenanceList)
                .Include(a => a.AttractionVisitorsList)
                .SingleOrDefaultAsync(m => m.Id == id);

            }
            else
            {
                Attraction = await _context.Attractions
                .Include(a => a.AttractionType)
                .Include(a => a.Locaiton)
                .Include(a => a.ParkInfo).SingleOrDefaultAsync(m => m.Id == id);
            }


            if (Attraction == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
