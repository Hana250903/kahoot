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
    public class QuestionSessionService : IQuestionSessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuestionSessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<bool> CreateQuestionSession(QuestionSessionModel questionSessionModel)
        {
            throw new NotImplementedException();
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
