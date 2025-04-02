using PRN222.Kahoot.Service.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.Services.Interfaces
{
    public interface IQuizService
    {
        Task<Pagination<QuizModel>> GetQuizs(PaginationModel paginationModel);
        Task<QuizModel> GetById(int id);
        Task<bool> CreateQuiz(QuizModel quizModel);
        Task<bool> UpdateQuiz(QuizModel quizModel);
        Task<bool> DeleteQuiz(int id);
    }
}
