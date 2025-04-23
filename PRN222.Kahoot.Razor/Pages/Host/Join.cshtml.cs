using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Services;
using PRN222.Kahoot.Service.Services.Interfaces;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Razor.Pages.Host
{
    [Authorize(Roles = "Admin")]
    public class JoinModel : PageModel
    {
        private readonly IQuizSessionService _quizSessionService;
        private readonly IHubContext<GameHub> _hubContext;
        private readonly IQuestionSessionService _questionSessionService;
        private readonly IMapper _mapper;

        public JoinModel(IQuizSessionService quizSessionService, IQuestionSessionService questionSessionService, IHubContext<GameHub> hubContext, IMapper mapper)
        {
            _quizSessionService = quizSessionService;
            _hubContext = hubContext;
            _questionSessionService = questionSessionService;
            _mapper = mapper;
        }
        public QuestionSession CurrentQuestion { get; set; }

        public QuestionRequestModel QuestionRequest; 

        public string Code { get; set; }
        public int TotalQuestions { get; set; }
        public int CurrentOrder { get; set; }

        public async Task<IActionResult> OnGet(string roomCode, int orderId = 1)
        {
            Code = roomCode;

            var quizSession = await _quizSessionService.GetByCode(roomCode);

            var question = await _questionSessionService.GetByQuizId(quizSession.SessionId);
            if (question == null)
            {
                return RedirectToPage("/Host/RoomEndGame", new { roomCode });
            }

            TotalQuestions = question.Count();

            CurrentOrder = orderId;

            if (CurrentOrder > TotalQuestions)
            {
                var roomExisting = await _quizSessionService.GetRoom(roomCode);
                if (roomExisting == null)
                {
                    return NotFound();
                }
                roomExisting.EndTime = DateTime.UtcNow.AddHours(7);
                roomExisting.IsActive = false;

                await _quizSessionService.UpdateQuizSession(roomExisting);
                await _hubContext.Clients.Group(roomCode).SendAsync("EndQuiz", roomCode);
                return RedirectToPage("/Host/RoomEndGame", new { roomCode });
            }

            var currentQuestion = question.FirstOrDefault(q => q.QuestionIndex == orderId);
            if (currentQuestion == null)
            {
                return NotFound();
            }

            var currentQuestionModel = _mapper.Map<QuestionSessionModel>(currentQuestion);
            currentQuestionModel.StartTime = DateTime.UtcNow.AddHours(7);
            currentQuestionModel.EndTime = DateTime.UtcNow.AddHours(7).AddSeconds(currentQuestion.Question.Duration);

            await _questionSessionService.UpdateQuestionSession(currentQuestionModel);

            var currentQuestionSession = await _questionSessionService.GetById(currentQuestionModel.QuestionSessionId);

            CurrentQuestion = currentQuestionSession;

            QuestionRequest = new QuestionRequestModel
            {
                QuestionText = currentQuestion.Question.QuestionText,
                AnswerA = currentQuestion.Question.Question1,
                AnswerB = currentQuestion.Question.Question2,
                AnswerC = currentQuestion.Question.Question3,
                AnswerD = currentQuestion.Question.Question4,
                Answer = currentQuestion.Question.Answer,
                TimeLimit = currentQuestion.Question.Duration,
                QuestionIndex = currentQuestion.QuestionIndex
            };

            await Task.Delay(2000);

            await _hubContext.Clients.Group(roomCode).SendAsync("SendQuestion", QuestionRequest);

            return Page();
        }

        public async Task<IActionResult> OnPostNextQuestionAsync(string code, int currentOrder)
        {
            // Tăng order để chuyển sang câu tiếp theo
            return RedirectToPage("/Host/Join", new { roomCode = code, orderId = currentOrder + 1 });
        }
    }
}
