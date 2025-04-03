using PRN222.Kahoot.Service.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.Services.Interfaces
{
    public interface IQuizSessionService
    {
        Task<Pagination<QuizSessionModel>> GetQuizSessions(PaginationModel paginationModel);
        Task<QuizSessionModel> GetById(int id);
        Task<bool> CreateQuizSession(QuizSessionModel quizSessionModel);
        Task<bool> UpdateQuizSession(QuizSessionModel quizSessionModel);
        Task<bool> DeleteQuizSession(int id);

        Task<QuizModel> QuizDetails(int id);
    }
}
