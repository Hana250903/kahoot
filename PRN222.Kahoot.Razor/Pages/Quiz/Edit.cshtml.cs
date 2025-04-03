using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Services.Interfaces;

namespace PRN222.Kahoot.Razor.Pages.Quiz
{
    public class EditModel : PageModel
    {
		private IQuizService _quizService;

		public EditModel(IQuizService quizService)
        {
			_quizService = quizService;
		}

        [BindProperty]
        public QuizModel Quiz { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz =  await _quizService.GetById((int)id);
			if (quiz == null)
            {
                return NotFound();
            }
            Quiz = quiz;
           //ViewData["CreateBy"] = new SelectList(_context.Users, "UserId", "Role");
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

            try
            {
                await _quizService.UpdateQuiz(Quiz);
			}
            catch (DbUpdateConcurrencyException)
            {
				var quiz = await _quizService.GetById(Quiz.QuizId);
				if (quiz == null)
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
    }
}
