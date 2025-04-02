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
    public class DeleteModel : PageModel
    {
        private readonly PRN222.Kahoot.Repository.Models.KahootContext _context;

        public DeleteModel(PRN222.Kahoot.Repository.Models.KahootContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizsession = await _context.QuizSessions.FindAsync(id);
            if (quizsession != null)
            {
                QuizSession = quizsession;
                _context.QuizSessions.Remove(QuizSession);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
