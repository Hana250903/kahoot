using PRN222.Kahoot.Service.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.Services.Interfaces
{
    public interface IParticipantService
    {
        Task<Pagination<ParticipantModel>> GetParticipants(PaginationModel paginationModel);
        Task<ParticipantModel> GetById(int id);
        Task<bool> CreateParticipant(ParticipantModel participantModel);
        Task<bool> UpdateParticipant(ParticipantModel participantModel);
        Task<bool> DeleteParticipant(int id);
    }
}
