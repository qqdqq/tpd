using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using TPD.Authorization;
using TPD.Data;
using TPD.Models;

namespace TPD.Pages.Attractions
{
    public class IndexModel : DI_BasePageModel
    {
        private readonly TPD.Data.ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context,
                        IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager) : base(context, authorizationService, userManager)
        {
            _context = context;
        }

        public IList<Attraction> Attraction { get;set; }

        public async Task OnGetAsync()
        {

            var isManager = User.IsInRole(Constants.AttractionManagerRole);
            var isAdmin = User.IsInRole(Constants.AttractionAdministratorsRole);

            if (isManager || isAdmin)
            {
                Attraction = await _context.Attractions
                    .Include(a => a.AttractionType)
                    .Include(a => a.Locaiton)
                    .Include(a => a.ParkInfo)
                    .Include(a => a.AttractionVisitorsList).ToListAsync();
            }
            else
            {
                Attraction = await _context.Attractions
                    .Include(a => a.AttractionType)
                     .Include(a => a.Locaiton)
                     .Include(a => a.ParkInfo)
                     .Include(a => a.AttractionVisitorsList).ToListAsync();
            }
        }
    }
}
