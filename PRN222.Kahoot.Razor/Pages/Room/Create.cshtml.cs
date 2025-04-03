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

namespace PRN222.Kahoot.Razor.Pages.Room
{
    public class CreateModel : PageModel
    {
        private readonly IQuizSessionService _quizSessionService;
        private readonly IQuizService _quizService;

        public CreateModel(IQuizSessionService quizSessionService, IQuizService quizService)
        {
            _quizSessionService = quizSessionService;
            _quizService = quizService;
        }

        public async Task<IActionResult> OnGet()
        {

        ViewData["QuizId"] = new SelectList(await _quizService.GetQuizs(PaginationModel = null), "QuizId", "Title");
            return Page();
        }

        [BindProperty]
        public QuizSessionModel QuizSession { get; set; } = default!;

        public PaginationModel PaginationModel { get; set; } = new PaginationModel();

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var session = await _quizSessionService.CreateQuizSession(QuizSession.QuizId, QuizSession.HostId);

                TempData["SuccessMessage"] = "Room created successfully! Code: " + session.CodeRoom;
                return RedirectToPage("./Details", new { sessionId = session.SessionId });
            }
            catch (Exception ex)
            {
                ViewData["QuizId"] = new SelectList(await _quizService.GetQuizs(PaginationModel = null), "QuizId", "Title");
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
