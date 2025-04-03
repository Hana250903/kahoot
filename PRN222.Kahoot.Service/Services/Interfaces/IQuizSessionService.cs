using PRN222.Kahoot.Repository.Models;
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
        Task<QuizSession> CreateQuizSession(int quizId, int hostId);
        Task<QuizSessionModel> GetById(int id);
        Task<QuizSessionModel> GetRoom(string code);
        Task<bool> UpdateQuizSession(QuizSessionModel quizSessionModel);
        Task<QuizSession> GetByCode(string code);
    }
}
