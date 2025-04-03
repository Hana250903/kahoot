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
    public class QuizService : IQuizService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuizService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateQuiz(QuizModel quizModel)
        {
           var quiz = _mapper.Map<Quiz>(quizModel);
			await _unitOfWork.QuizRepository.AddAsync(quiz);
			await _unitOfWork.SaveChangeAsync();
			return true;
		}

        public async Task<bool> DeleteQuiz(int id)
        {
			var quiz = await _unitOfWork.QuizRepository.FindAsync(c => c.QuizId == id);
			await _unitOfWork.QuizRepository.DeletedAsync(quiz);
			await _unitOfWork.SaveChangeAsync();
			return true;
		}

        public async Task<QuizModel> GetById(int id)
        {
            var quiz = await _unitOfWork.QuizRepository.FindAsync(c => c.QuizId == id);
			if (quiz == null)
			{
				return null;
			}
			var model = _mapper.Map<QuizModel>(quiz);

			return model;
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

        public async Task<bool> UpdateQuiz(QuizModel quizModel)
        {
            var quiz = await _unitOfWork.QuizRepository.FindAsync(c => c.QuizId == quizModel.QuizId);
			if (quiz == null)
			{
				return false;
			}

			var quizUpdate = _mapper.Map<Quiz>(quizModel);

			await _unitOfWork.QuizRepository.UpdateAsync(quizUpdate);
			await _unitOfWork.SaveChangeAsync();
			return true;
		}
    }
}
