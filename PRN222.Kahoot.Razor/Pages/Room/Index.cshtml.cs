using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Repository.Models;

namespace PRN222.Kahoot.Razor.Pages.Room
{
    public class IndexModel : PageModel
    {
        private readonly PRN222.Kahoot.Repository.Models.KahootContext _context;

        public IndexModel(PRN222.Kahoot.Repository.Models.KahootContext context)
        {
            _context = context;
        }

        public IList<QuizSession> QuizSession { get;set; } = default!;

        public async Task OnGetAsync()
        {
            QuizSession = await _context.QuizSessions
                .Include(q => q.Host)
                .Include(q => q.Quiz).ToListAsync();
        }
    }
}
