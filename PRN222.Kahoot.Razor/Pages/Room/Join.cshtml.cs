using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Services;
using PRN222.Kahoot.Service.Services.Interfaces;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Razor.Pages.Room
{
    public class JoinModel : PageModel
    {
        private readonly IQuizSessionService _quizSessionService;
        private readonly IHubContext<GameHub> _hubContext;
        private readonly IQuestionService _questionService;

        public JoinModel(IQuizSessionService quizSessionService, IQuestionService questionService, IHubContext<GameHub> hubContext)
        {
            _quizSessionService = quizSessionService;
            _hubContext = hubContext;
            _questionService = questionService;
        }

        [BindProperty]
        public QuizSessionModel Session { get; set; }
        public List<ParticipantModel> Players { get; set; }
        public List<QuestionModel> Questions { get; set; }
        public QuestionModel CurrentQuestion { get; set; }

        public async Task<IActionResult> OnGet(string code)
        {
            var session  = await _quizSessionService.GetRoom(code);
            if (session == null)
            {
                return NotFound();
            }
            Session = session;
            //Players = session.Participants;
            Questions = await _questionService.GetQuestionByQuizId(session.QuizId);
            CurrentQuestion = Questions.FirstOrDefault();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var session = await _quizSessionService.GetById(Session.SessionId);
            if (session == null) return NotFound();

            await _quizSessionService.UpdateQuizSession(session);

            await _hubContext.Clients.Group(session.CodeRoom).SendAsync("StartGame");

            // Gọi StartTimer với Duration của CurrentQuestion
            if (CurrentQuestion != null)
            {
                await _hubContext.Clients.Group(session.CodeRoom).SendAsync("StartTimer", session.CodeRoom, CurrentQuestion.Duration);
            }

            return RedirectToPage("Join", new Dictionary<string, string> { { "code", session.CodeRoom } });
        }
    }
}
