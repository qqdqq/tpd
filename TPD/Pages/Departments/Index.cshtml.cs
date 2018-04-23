using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TPD.Data;
using TPD.Models;

namespace TPD.Pages.Departments
{
    public class IndexModel : PageModel
    {
        private readonly TPD.Data.ApplicationDbContext _context;

        public IndexModel(TPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Department> Department { get;set; }

        public async Task OnGetAsync()
        {
            Department = await _context.Departments
                .Include(d => d.Location)
                .Include(d => d.ParkInfo).ToListAsync();
        }
    }
}
