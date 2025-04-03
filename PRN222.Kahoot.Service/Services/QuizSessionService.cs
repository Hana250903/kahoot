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

        public QuizSessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateQuizSession(QuizSessionModel quizSessionModel)
        {
            var quizSession = _mapper.Map<QuizSession>(quizSessionModel);
            quizSession.StartTime = DateTime.UtcNow.AddHours(7);
            await _unitOfWork.QuizSessionRepository.AddAsync(quizSession);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public Task<bool> DeleteQuizSession(int id)
        {
            throw new NotImplementedException();
        }

        public Task<QuizSessionModel> GetById(int id)
        {
            throw new NotImplementedException();
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

        public Task<bool> UpdateQuizSession(QuizSessionModel quizSessionModel)
        {
            throw new NotImplementedException();
        }

        public async Task<QuizModel> QuizDetails(int id)
        {
            //var list = await _unitOfWork.QuizRepository.GetAsync(c => c.QuizId == id, include: c => c.Include(i => i.Questions));

            //var quiz = list.Select( q => new
            //{
            //    TotalQuestion = q.Questions.Count(),
            //    TotalScore = q.Questions.Sum(question => question.)
            //}).FirstOrDefault();

            throw new NotImplementedException();
        }
    }
}
