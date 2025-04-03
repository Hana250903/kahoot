using PRN222.Kahoot.Service.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<Pagination<QuestionModel>> GetQuestions(PaginationModel paginationModel);
        Task<QuestionModel> GetById(int id);
        Task<bool> CreateQuestion(QuestionModel questionModel);
        Task<bool> UpdateQuestion(QuestionModel questionModel);
        Task<bool> DeleteQuestion(int id);
        Task<List<QuestionModel>> GetQuestionByQuizId(int quizId);
    }
}
