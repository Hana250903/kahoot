using AutoMapper;
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

        public Task<bool> CreateQuizSession(QuizSessionModel quizSessionModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteQuizSession(int id)
        {
            throw new NotImplementedException();
        }

        public Task<QuizSessionModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Pagination<QuizSessionModel>> GetQuizSessions(PaginationModel paginationModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateQuizSession(QuizSessionModel quizSessionModel)
        {
            throw new NotImplementedException();
        }
    }
}
