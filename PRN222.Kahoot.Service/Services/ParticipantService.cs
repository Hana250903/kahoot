using AutoMapper;
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
    public class ParticipantService : IParticipantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ParticipantService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<bool> CreateParticipant(ParticipantModel participantModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteParticipant(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ParticipantModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Pagination<ParticipantModel>> GetParticipants(PaginationModel paginationModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateParticipant(ParticipantModel participantModel)
        {
            throw new NotImplementedException();
        }
    }
}
