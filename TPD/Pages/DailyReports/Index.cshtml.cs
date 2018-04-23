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
    public class IndexModel : PageModel
    {
        private readonly TPD.Data.ApplicationDbContext _context;

        public IndexModel(TPD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<DailyReport> DailyReport { get;set; }

        public async Task OnGetAsync()
        {
            DailyReport = await _context.DailyReports
                .Include(d => d.Weather).ToListAsync();
        }
    }
}
