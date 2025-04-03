using AutoMapper;
using PRN222.Kahoot.Repository.UnitOfWork;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Interfaces;
using PRN222.Kahoot.Service.Services.Interfaces;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SessionModel> GetSessionByCodeAsync(string sessionCode)
        {
            var entity = await _unitOfWork.QuizSessionRepository
                .FindAsync(s => s.CodeRoom == sessionCode); // Đổi từ SessionCode thành CodeRoom
            return _mapper.Map<SessionModel>(entity);
        }
    }
}