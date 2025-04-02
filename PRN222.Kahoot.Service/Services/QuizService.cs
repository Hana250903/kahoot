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
    public class QuizService : IQuizService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuizService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<bool> CreateQuiz(QuizModel quizModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteQuiz(int id)
        {
            throw new NotImplementedException();
        }

        public Task<QuizModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Pagination<QuizModel>> GetQuizs(PaginationModel? paginationModel)
        {
            var quizs = await _unitOfWork.QuizRepository.GetAsync();

            var result = quizs.Select(c =>
                new QuizModel
                {
                    QuizId = c.QuizId,
                    Title = c.Title,
                    Description = c.Description,
                    CreateAt = c.CreateAt,
                    CreateBy = c.CreateBy,
                    Visibility = c.Visibility,
                }).ToList();
            var count = quizs.Count();

            return new Pagination<QuizModel>(result, count);
        }

        public Task<bool> UpdateQuiz(QuizModel quizModel)
        {
            throw new NotImplementedException();
        }
    }
}
