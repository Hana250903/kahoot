using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Service.Interfaces;
using PRN222.Kahoot.Service.Services;
using PRN222.Kahoot.Service.Services.Interfaces;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace PRN222.Kahoot.Razor
{
    public class GameHub : Hub
    {
        private readonly IQuizService _quizService; // Inject your service to handle quiz logic
        private readonly IParticipantService _participantService;
        private readonly IMapper _mapper;
        private readonly IQuestionSessionService _questionSessionService;
        private readonly IQuizSessionService _quizSessionService;
        private readonly IResponseService _responseService;
        private static readonly ConcurrentDictionary<string, List<Participant>> RoomParticipants = new();

        public GameHub(IQuizService quizService, IParticipantService participantService, IMapper mapper, IQuestionSessionService questionSessionService, IQuizSessionService quizSessionService, IResponseService responseService)
        {
            _quizService = quizService;
            _participantService = participantService;
            _mapper = mapper;
            _questionSessionService = questionSessionService;
            _quizSessionService = quizSessionService;
            _responseService = responseService;
        }

        public async Task JoinRoom(string code, int playerId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, code);
            Console.WriteLine($"Added {Context.ConnectionId} (Player {playerId}) to Room_{code}");

            if (playerId == 0)
            {
                Console.WriteLine("Invalid playerId.");
                return;
            }

            // Lấy thông tin người chơi từ ParticipantService
            var playerModel = await _participantService.GetByIdAsync(playerId);
            if (playerModel == null)
            {
                Console.WriteLine($"Player {playerId} not found.");
                return;
            }
            var player = _mapper.Map<Participant>(playerModel);

            // Cập nhật danh sách người chơi trong phòng
            RoomParticipants.AddOrUpdate(
                code,
                new List<Participant> { player },
                (key, oldValue) =>
                {
                    if (!oldValue.Any(p => p.ParticipantId == player.ParticipantId))
                    {
                        oldValue.Add(player);
                    }
                    return oldValue;
                });

            // Lấy danh sách người chơi trong phòng sau khi cập nhật
            var playersInRoomUpdated = RoomParticipants.GetOrAdd(code, new List<Participant>());

            // Gửi danh sách người chơi cập nhật tới tất cả client trong phòng
            await Clients.Group(code).SendAsync("UpdatePlayers", playersInRoomUpdated);
        }

        public async Task QuitRoom(string code, int playerId)
        {
            // Xóa client khỏi nhóm phòng
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, code);

            // Cập nhật danh sách người chơi
            if (RoomParticipants.TryGetValue(code, out var players))
            {
                var playerToRemove = players.FirstOrDefault(p => p.ParticipantId == playerId);
                if (playerToRemove != null)
                {
                    players.Remove(playerToRemove);
                }

                // Gửi danh sách người chơi cập nhật
                await Clients.Group(code).SendAsync("UpdatePlayers", players);
            }
        }

        public async Task SubmitAnswer(int sessionId, int playerId, int selectedAnswer, int questionIndex)
        {
            Console.WriteLine($"Player {playerId} submitted answer {selectedAnswer} for Session_{sessionId}, Question {questionIndex}");

            // Lấy thông tin phiên chơi
            var quizSessionModel = await _quizSessionService.GetById(sessionId);
            if (quizSessionModel == null)
            {
                Console.WriteLine("Quiz session not found.");
                return;
            }
            var quizSession = _mapper.Map<QuizSession>(quizSessionModel);

            // Lấy câu hỏi trong phiên chơi dựa trên index
            var questionSession = quizSession.QuestionSessions.FirstOrDefault(q => q.QuestionIndex == questionIndex);
            if (questionSession == null)
            {
                Console.WriteLine("Question not found in session.");
                return;
            }

            // Kiểm tra đáp án đúng
            bool isCorrect = questionSession.Question.Answer == selectedAnswer;

            // Tính thời gian phản hồi
            TimeSpan responseTime = DateTime.Now - questionSession.StartTime;

            // Tạo phản hồi mới
            var response = new Response
            {
                ParticipantId = playerId,
                QuestionSessionId = questionSession.QuestionSessionId,
                IsCorrect = isCorrect,
                AnsweredAt = DateTime.Now
            };

            // Lưu phản hồi vào DB
            //await _responseService.CreateResponse(response);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // Xử lý khi client ngắt kết nối (tự động rời phòng)
            foreach (var room in RoomParticipants)
            {
                var player = room.Value.FirstOrDefault(p => p.ParticipantId.ToString() == Context.ConnectionId);
                if (player != null)
                {
                    await QuitRoom(room.Key, player.ParticipantId);
                    break;
                }
            }
            await base.OnDisconnectedAsync(exception);
        }

    }


}
