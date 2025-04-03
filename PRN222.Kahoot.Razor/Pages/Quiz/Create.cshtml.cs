using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Services.Interfaces;

namespace PRN222.Kahoot.Razor.Pages.Quiz
{
    public class CreateModel : PageModel
    {
        private readonly IQuizService _quizService;
		public CreateModel(IQuizService quizService)
        {
			_quizService = quizService;
		}

        public IActionResult OnGet()
        {
        //ViewData["CreateBy"] = new SelectList(_userService., "UserId", "Role");
            return Page();
        }

        [BindProperty]
        public QuizModel Quiz { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _quizService.CreateQuiz(Quiz);

            return RedirectToPage("./Index");
        }
    }
}
