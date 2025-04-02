using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Services;
using PRN222.Kahoot.Service.Services.Interfaces;

namespace PRN222.Kahoot.Razor.Pages.Question
{
    public class CreateModel : PageModel
    {
        private readonly IQuestionService _questionService;

        public CreateModel(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public IActionResult OnGet()
        {
        //ViewData["QuizId"] = new SelectList(_context.Quizzes, "QuizId", "Title");
            return Page();
        }

        [BindProperty]
        public QuestionModel Question { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _questionService.CreateQuestion(Question);

            return RedirectToPage("./Index");
        }
    }
}
