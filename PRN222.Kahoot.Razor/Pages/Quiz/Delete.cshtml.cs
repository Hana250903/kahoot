using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Services.Interfaces;

namespace PRN222.Kahoot.Razor.Pages.Quiz
{
    public class DeleteModel : PageModel
    {
        private readonly IQuizService _quizService;

		public DeleteModel(IQuizService quizService)
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

            var quiz = await _quizService.GetById((int)id);

            if (quiz is not null)
            {
                Quiz = quiz;

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

			var quiz = await _quizService.GetById((int)id);
			if (quiz != null)
            {
                Quiz = quiz;
                await _quizService.DeleteQuiz((int)id);
            }

            return RedirectToPage("./Index");
        }
    }
}
