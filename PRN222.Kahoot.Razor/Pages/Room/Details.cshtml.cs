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
    public class DetailsModel : PageModel
    {
        private readonly PRN222.Kahoot.Repository.Models.KahootContext _context;

        public DetailsModel(PRN222.Kahoot.Repository.Models.KahootContext context)
        {
            _context = context;
        }

        public QuizSession QuizSession { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizsession = await _context.QuizSessions.FirstOrDefaultAsync(m => m.SessionId == id);

            if (quizsession is not null)
            {
                QuizSession = quizsession;

                return Page();
            }

            return NotFound();
        }
    }
}
