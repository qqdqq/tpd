using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TPD.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using TPD.Authorization;
using TPD.Models;

namespace TPD.Pages.Attractions
{
    public class CreateModel : DI_BasePageModel
    {
        private readonly TPD.Data.ApplicationDbContext _context;

        public CreateModel(
            ApplicationDbContext context, 
            IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager) : base(context, authorizationService, userManager)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AttractionTypeId"] = new SelectList(_context.AttractionTypes, "Id", "Name");
        ViewData["LocationId"] = new SelectList(_context.Locaitons, "Id", "Name");
        ViewData["ParkInfoId"] = new SelectList(_context.ParkInfo, "Id", "Country");
            return Page();
        }

        [BindProperty]
        public Attraction Attraction { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                           User, Attraction,
                                                           AttractionOperations.Create);

            if (!isAuthorized.Succeeded) { return new ChallengeResult(); }

            _context.Attractions.Add(Attraction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}