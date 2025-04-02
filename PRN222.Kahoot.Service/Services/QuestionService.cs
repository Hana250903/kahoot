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
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateQuestion(QuestionModel questionModel)
        {
            var question = _mapper.Map<Question>(questionModel);
            await _unitOfWork.QuestionRepository.AddAsync(question);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<bool> DeleteQuestion(int id)
        {
            var question = await _unitOfWork.QuestionRepository.FindAsync(c => c.QuestionId == id);
            if (question == null)
            {
                return false;
            }

            await _unitOfWork.QuestionRepository.DeletedAsync(question);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<QuestionModel> GetById(int id)
        {
            var question = await _unitOfWork.QuestionRepository.FindAsync(c => c.QuestionId == id);
            if (question == null)
            {
                return null;
            }
            var model = _mapper.Map<QuestionModel>(question);

            return model;
        }

        public async Task<Pagination<QuestionModel>> GetQuestions(PaginationModel paginationModel)
        {
            var questions = await _unitOfWork.QuestionRepository.GetAsync();

            var result = questions.Select(c => 
                new QuestionModel
                {
                    QuestionId = c.QuestionId,
                    QuizId = c.QuizId,
                    QuestionText = c.QuestionText,
                    QuestionType = c.QuestionType,
                    Duration = c.Duration,
                    Question1 = c.Question1,
                    Question2 = c.Question2,
                    Question3 = c.Question3,
                    Question4 = c.Question4,
                    Answer = c.Answer
                }).Skip((paginationModel.PageIndex - 1) * paginationModel.PageSize)
                .Take(paginationModel.PageSize).ToList();

            var count = questions.Count();

            return new Pagination<QuestionModel>(result, count);
        }

        public async Task<bool> UpdateQuestion(QuestionModel questionModel)
        {
            var question = await _unitOfWork.QuestionRepository.FindAsync(c => c.QuestionId == questionModel.QuestionId);
            if (question == null)
            {
                return false;
            }
            var updateQuestion = _mapper.Map<Question>(questionModel);

            await _unitOfWork.QuestionRepository.UpdateAsync(updateQuestion);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
