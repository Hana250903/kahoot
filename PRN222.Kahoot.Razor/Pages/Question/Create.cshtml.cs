using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Services;
using PRN222.Kahoot.Service.Services.Interfaces;

namespace PRN222.Kahoot.Razor.Pages.Question
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IQuestionService _questionService;
        private readonly IQuizService _quizService;
        private readonly IHubContext<GameHub> _hubContext;

        public CreateModel(IQuestionService questionService, IQuizService quizService, IHubContext<GameHub> hubContext)
        {
            _questionService = questionService;
            _quizService = quizService;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["QuizId"] = new SelectList(await _quizService.GetQuizs(paginationModel : null), "QuizId", "Title");
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
            await _hubContext.Clients.All.SendAsync("LoadAllItems");

            return RedirectToPage("./Index");
        }
    }
}
