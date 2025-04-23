using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Services;
using PRN222.Kahoot.Service.Services.Interfaces;

namespace PRN222.Kahoot.Razor.Pages.Question
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IQuestionService _questionService;

        public EditModel(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [BindProperty]
        public QuestionModel Question { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question =  await _questionService.GetById((int)id);
            if (question == null)
            {
                return NotFound();
            }
            Question = question;
           //ViewData["QuizId"] = new SelectList(_context.Quizzes, "QuizId", "Title");
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
                await _questionService.UpdateQuestion(Question);
            }
            catch (DbUpdateConcurrencyException)
            {
                var question = await _questionService.GetById(Question.QuestionId);
                if (question == null)
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
