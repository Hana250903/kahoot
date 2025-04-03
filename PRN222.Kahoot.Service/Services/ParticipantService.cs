using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Repository.UnitOfWork;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ParticipantService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ParticipantModel> JoinSessionAsync(ParticipantModel model)
        {
            var entity = _mapper.Map<Participant>(model);
            entity.JoinAt = DateTime.Now;
            entity.Score = 0; // Điểm ban đầu
            await _unitOfWork.ParticipantRepository.AddAsync(entity);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<ParticipantModel>(entity);
        }

        public async Task<ParticipantModel> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.ParticipantRepository
                .FindAsync(p => p.ParticipantId == id,
                    include: q => q.Include(p => p.User)
                                   .Include(p => p.Session));
            return _mapper.Map<ParticipantModel>(entity);
        }

        public async Task<IEnumerable<ParticipantModel>> GetAllAsync()
        {
            var entities = await _unitOfWork.ParticipantRepository
                .GetAsync(include: q => q.Include(p => p.User)
                                         .Include(p => p.Session));
            return _mapper.Map<List<ParticipantModel>>(entities);
        }

        public async Task UpdateScoreAsync(int participantId, int score)
        {
            var entity = await _unitOfWork.ParticipantRepository
                .FindAsync(p => p.ParticipantId == participantId);
            if (entity != null)
            {
                entity.Score = (entity.Score ?? 0) + score;
                await _unitOfWork.ParticipantRepository.UpdateAsync(entity);
                await _unitOfWork.SaveChangeAsync();
            }
        }
    }
}