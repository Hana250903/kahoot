using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Services;
using PRN222.Kahoot.Service.Services.Interfaces;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Razor.Pages.Host
{
    [Authorize(Roles = "Admin")]
    public class RoomModel : PageModel
    {
        private readonly IQuizSessionService _quizSessionService;
        private readonly IMapper _mapper;
        private readonly IQuestionSessionService _questionSessionService;
        private readonly IHubContext<GameHub> _hubContext;
        private readonly IQuestionService _questionService;

        public RoomModel(IQuizSessionService quizSessionService, IMapper mapper, IQuestionSessionService questionSessionService, IHubContext<GameHub> hubContext, IQuestionService questionService)
        {
            _quizSessionService = quizSessionService;
            _mapper = mapper;
            _questionSessionService = questionSessionService;
            _hubContext = hubContext;
            _questionService = questionService;
        }   

        public QuizSession QuizSession { get; set; }

        public List<ParticipantModel> Participants { get; set; }

        public async Task<IActionResult> OnGet(string roomCode)
        {
            var response = await _quizSessionService.GetByCode(roomCode);
            if (response == null)
            {
                return NotFound();
            }

            QuizSession = response;

            Participants = new List<ParticipantModel>();
            foreach (var participant in QuizSession.Participants)
            {
                var participantModel = new ParticipantModel
                {
                    ParticipantId = participant.ParticipantId,
                    Team = participant.Team,
                    Score = participant.Score,
                    JoinAt = participant.JoinAt,
                };
                Participants.Add(participantModel);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostStartQuizAsync(string code)
        {
            var quizSession = await _quizSessionService.GetByCode(code);

            if (quizSession == null)
            {
                return NotFound();
            }

            quizSession.StartTime = DateTime.UtcNow.AddHours(7);
            var quizSessionModel = _mapper.Map<QuizSessionModel>(quizSession);

            await _quizSessionService.UpdateQuizSession(quizSessionModel);

            var questionSession = await _questionService.GetQuestionByQuizId(quizSession.QuizId);
            if (questionSession == null)
            {
                return NotFound();
            }

            int index = 1;
            var questionSessionRequest = questionSession.Select(q => new QuestionSession
            {
                QuestionId = q.QuestionId,
                QuizSessionId = quizSessionModel.SessionId,
                QuestionIndex = index++,
                Point = 10,
                StartTime = DateTime.UtcNow.AddHours(7),
                EndTime = DateTime.UtcNow.AddHours(7).AddMinutes(q.Duration),
            }).ToList();

            var questionSessionModel = _mapper.Map<List<QuestionSessionModel>>(questionSessionRequest);

            var result = await _questionSessionService.CreateQuestionSession(questionSessionModel);

            await _hubContext.Clients.Group(code).SendAsync("QuizStarted", code);

            return RedirectToPage("/Host/Join", new { roomCode = code, orderId = 1 });
        }
    }
}
