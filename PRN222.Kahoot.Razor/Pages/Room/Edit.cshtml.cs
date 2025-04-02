using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Repository.Models;

namespace PRN222.Kahoot.Razor.Pages.Room
{
    public class EditModel : PageModel
    {
        private readonly PRN222.Kahoot.Repository.Models.KahootContext _context;

        public EditModel(PRN222.Kahoot.Repository.Models.KahootContext context)
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

            var quizsession =  await _context.QuizSessions.FirstOrDefaultAsync(m => m.SessionId == id);
            if (quizsession == null)
            {
                return NotFound();
            }
            QuizSession = quizsession;
           ViewData["HostId"] = new SelectList(_context.Users, "UserId", "Role");
           ViewData["QuizId"] = new SelectList(_context.Quizzes, "QuizId", "Title");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(QuizSession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizSessionExists(QuizSession.SessionId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool QuizSessionExists(int id)
        {
            return _context.QuizSessions.Any(e => e.SessionId == id);
        }
    }
}
