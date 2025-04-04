using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Repository.UnitOfWork;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
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

        public async Task<bool> CreateQuestionSession(List<QuestionSessionModel> questionSessionModel)
        {
            var questionSessionEntities = _mapper.Map<List<QuestionSession>>(questionSessionModel);

            await _unitOfWork.QuestionSessionRepository.AddRangeAsync(questionSessionEntities);
            
            await _unitOfWork.SaveChangeAsync();
            Console.WriteLine("Question sessions created successfully.");

            return true;
        }

        public async Task<QuestionSession> GetById(int id)
        {
            var questionSession = await _unitOfWork.QuestionSessionRepository.FindAsync(c => c.QuestionSessionId == id,
                i => i.Include(q => q.Question));
            if (questionSession == null)
            {
                return null;
            }

            return questionSession;
        }

        public Task<Pagination<QuestionSessionModel>> GetQuestionSessions()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateQuestionSession(QuestionSessionModel questionSessionModel)
        {
            var questtionSession = _mapper.Map<QuestionSession>(questionSessionModel);

            await _unitOfWork.QuestionSessionRepository.UpdateAsync(questtionSession);
            await _unitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<List<QuestionSession>> GetByQuizId(int quizId)
        {
            var questionSession = await _unitOfWork.QuestionSessionRepository.GetAsync(q => q.QuizSessionId == quizId,
                i => i.Include(q => q.Question));
            if (questionSession == null)
            {
                return null;
            }

            return questionSession;
        }

        public async Task<List<QuestionSession>> GetByCode(string code)
        {
            var questionSession = await _unitOfWork.QuestionSessionRepository.GetAsync(c => c.QuizSession.CodeRoom == code);

            if (questionSession == null)
            {
                return null;
            }
            return questionSession;
        }

        public Task<bool> DeleteQuestionSession(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<QuestionSession>> GetByQuizSessionId(int quizSessionId)
        {
            var questionSession = await _unitOfWork.QuestionSessionRepository.GetAsync(c => c.QuizSessionId == quizSessionId,
                i => i.Include(q => q.Question));

            if (questionSession == null)
            {
                return null;
            }
            return questionSession;
        }
    }
}
