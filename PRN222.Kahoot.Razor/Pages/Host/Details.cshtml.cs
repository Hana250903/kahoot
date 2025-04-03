using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Services;
using PRN222.Kahoot.Service.Services.Interfaces;

namespace PRN222.Kahoot.Razor.Pages.Host
{
    public class DetailsModel : PageModel
    {
        private readonly IQuizSessionService _quizSessionService;

        public DetailsModel(IQuizSessionService quizSessionService)
        {
            _quizSessionService = quizSessionService;
        }

        public QuizSessionModel QuizSession { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? sessionId)
        {
            if (sessionId == null)
            {
                return NotFound();
            }

            var quizsession = await _quizSessionService.GetById((int)sessionId);

            if (quizsession is not null)
            {
                QuizSession = quizsession;

                return Page();
            }

            return NotFound();
        }

        public IActionResult OnPostJoinRoom(string code)
        {
            return RedirectToPage("/Host/Room", new { roomCode = code });
        }
    }
}
