using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Repository.UnitOfWork;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.Services
{
    public class QuizSessionService : IQuizSessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IQuestionSessionService _questionSessionService;

        public QuizSessionService(IUnitOfWork unitOfWork, IMapper mapper, IQuestionSessionService questionSessionService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _questionSessionService = questionSessionService;
        }

        public async Task<QuizSession> CreateQuizSession(int quizId, int hostId)
        {
            try
            {
                var quiz = await _unitOfWork.QuizRepository.FindAsync(c => c.QuizId == quizId);
                if (quiz == null)
                {
                    throw new Exception("Quiz not found");
                }

                var questions = await _unitOfWork.QuestionRepository.GetAsync(c => c.QuizId == quizId);
                if (!questions.Any())
                {
                    throw new Exception("Quiz has no Questions!");
                }

                var sessionModel = new QuizSessionModel
                {
                    QuizId = quizId,
                    HostId = hostId,
                    CodeRoom = GenerateRoomCode(),
                    StartTime = DateTime.Now,
                    IsActive = false,
                    TotalQuestion = questions.Count,
                    TotalScore = questions.Count * 10, // Giả định mỗi câu 10 điểm
                };

                var quizSession = _mapper.Map<QuizSession>(sessionModel);
                var questionModel = _mapper.Map<List<QuestionModel>>(questions);
                await _unitOfWork.QuizSessionRepository.AddAsync(quizSession);

                await _unitOfWork.SaveChangeAsync();

                return quizSession;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in CreateQuizSession: {ex.Message}", ex);
            }
        }

        public async Task<Pagination<QuizSessionModel>> GetQuizSessions(PaginationModel paginationModel)
        {
            var quizSessions = await _unitOfWork.QuizSessionRepository.GetAsync(include: c => c.Include(i => i.Quiz));

            var result = quizSessions.Select(c => new QuizSessionModel
            {
                SessionId = c.SessionId,
                HostId = c.HostId,
                QuizId = c.QuizId,
                CodeRoom = c.CodeRoom,
                StartTime = c.StartTime,
                EndTime = c.EndTime,
                IsActive = c.IsActive,
                TotalQuestion = c.TotalQuestion,
                TotalScore = c.TotalScore,
                QuizTitle = c.Quiz.Title,
            }).Skip((paginationModel.PageIndex - 1) * paginationModel.PageSize).Skip(paginationModel.PageSize).ToList();

            return new Pagination<QuizSessionModel>(result, quizSessions.Count());
        }

        public async Task<QuizSessionModel> GetById(int id)
        {
            var quizSession = await _unitOfWork.QuizSessionRepository.FindAsync(c => c.SessionId == id, c => c.Include(c => c.Quiz));
            if (quizSession == null)
            {
                throw new Exception("Quiz session not found");
            }

            var quizSessionModel = new QuizSessionModel
            {
                SessionId = quizSession.SessionId,
                HostId = quizSession.HostId,
                QuizId = quizSession.QuizId,
                CodeRoom = quizSession.CodeRoom,
                StartTime = quizSession.StartTime,
                EndTime = quizSession.EndTime,
                IsActive = quizSession.IsActive,
                TotalQuestion = quizSession.TotalQuestion,
                TotalScore = quizSession.TotalScore,
                QuizTitle = quizSession.Quiz.Title,
            };

            return quizSessionModel;
        }

        private string GenerateRoomCode()
        {
            return Guid.NewGuid().ToString().Substring(0, 5).ToUpper(); // Tạo mã phòng 5 ký tự
        }

        public async Task<QuizSessionModel> GetRoom(string code)
        {
            var quizSession = await _unitOfWork.QuizSessionRepository.FindAsync(c => c.CodeRoom == code, c => c.Include(c => c.Quiz));

            if (quizSession == null)
            {
                throw new Exception("Room not found");
            }
            var quizSessionModel = new QuizSessionModel
            {
                SessionId = quizSession.SessionId,
                HostId = quizSession.HostId,
                QuizId = quizSession.QuizId,
                CodeRoom = quizSession.CodeRoom,
                StartTime = quizSession.StartTime,
                EndTime = quizSession.EndTime,
                IsActive = quizSession.IsActive,
                TotalQuestion = quizSession.TotalQuestion,
                TotalScore = quizSession.TotalScore,
                QuizTitle = quizSession.Quiz.Title,
            };

            return quizSessionModel;
        }

        public async Task<bool> UpdateQuizSession(QuizSessionModel quizSessionModel)
        {
            var quizSession = _mapper.Map<QuizSession>(quizSessionModel);
            quizSession.IsActive = true;
            quizSession.StartTime = DateTime.UtcNow.AddHours(7);

            await _unitOfWork.QuizSessionRepository.UpdateAsync(quizSession);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<QuizSession> GetByCode(string code)
        {
            var quizSession = await _unitOfWork.QuizSessionRepository.FindAsync(c => c.CodeRoom == code, i => i.Include(c => c.Quiz));
            if (quizSession == null)
            {
                throw new Exception("Quiz session not found");
            }
            return quizSession;
        }
    }
}
