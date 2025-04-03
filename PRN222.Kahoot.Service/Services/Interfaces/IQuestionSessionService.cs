using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Service.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.Services.Interfaces
{
    public interface IQuestionSessionService
    {
        Task<Pagination<QuestionSessionModel>> GetQuestionSessions();
        Task<QuestionSessionModel> GetById(int id);
        Task<bool> CreateQuestionSession(int quizSessionId, List<QuestionModel> questionModel);
        Task<bool> UpdateQuestionSession(QuestionSessionModel questionSessionModel);
        Task<bool> DeleteQuestionSession(int id);
    }
}
