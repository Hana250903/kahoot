using AutoMapper;
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
    public class QuestionSessionService : IQuestionSessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuestionSessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateQuestionSession(int quizSessionId, List<QuestionModel> questionModel)
        {
            int index = 1;
            var questionSessions = questionModel.Select(question => new QuestionSessionModel
            {
                QuestionId = question.QuestionId,
                QuizSessionId = quizSessionId,
                QuestionIndex = index++,
                Point = 10, // Giả định mỗi câu 10 điểm
                StartTime = DateTime.UtcNow.AddHours(7),
                EndTime = DateTime.UtcNow.AddHours(7).AddSeconds(question.Duration)
            }).ToList();

            var questionSessionEntities = _mapper.Map<List<QuestionSession>>(questionSessions);

            await _unitOfWork.QuestionSessionRepository.AddRangeAsync(questionSessionEntities);
            
            await _unitOfWork.SaveChangeAsync();
            Console.WriteLine("Question sessions created successfully.");

            return true;
        }

        public Task<bool> DeleteQuestionSession(int id)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionSessionModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Pagination<QuestionSessionModel>> GetQuestionSessions()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateQuestionSession(QuestionSessionModel questionSessionModel)
        {
            throw new NotImplementedException();
        }
    }
}
