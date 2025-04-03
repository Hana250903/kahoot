using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<QuestionSession> QuestionSessionRepository { get; }
        IGenericRepository<Participant> ParticipantRepository { get; }
        IGenericRepository<Question> QuestionRepository { get; }
        IGenericRepository<Quiz> QuizRepository { get; }
        IGenericRepository<QuizSession> QuizSessionRepository { get; }
        IGenericRepository<Response> ResponseRepository { get; }
        IGenericRepository<User> UserRepository { get; }

        Task<int> SaveChangeAsync();
        void ChangeTracker();

        void Detach<T>(T entity) where T : class;
    }
}
