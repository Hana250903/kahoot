using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KahootContext _context;

        public UnitOfWork(KahootContext context)
        {
            _context = context;
            QuestionSessionRepository = new GenericRepository<QuestionSession>(_context);
            ParticipantRepository = new GenericRepository<Participant>(_context);
            QuizRepository = new GenericRepository<Quiz>(_context);
            QuizSessionRepository = new GenericRepository<QuizSession>(_context);
            QuestionRepository = new GenericRepository<Question>(_context);
            ResponseRepository = new GenericRepository<Response>(_context);
            UserRepository = new GenericRepository<User>(_context);
        }

        public IGenericRepository<QuestionSession> QuestionSessionRepository { get; }

        public IGenericRepository<Participant> ParticipantRepository { get; }

        public IGenericRepository<Question> QuestionRepository { get; }

        public IGenericRepository<Quiz> QuizRepository { get; }

        public IGenericRepository<QuizSession> QuizSessionRepository { get; }

        public IGenericRepository<Response> ResponseRepository { get; }

        public IGenericRepository<User> UserRepository { get; }

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
