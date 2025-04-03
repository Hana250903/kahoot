using PRN222.Kahoot.Service.BusinessModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.Interfaces
{
    public interface IParticipantService
    {

    Task<ParticipantModel> JoinSessionAsync(ParticipantModel model);
        Task<ParticipantModel> GetByIdAsync(int id);
        Task<IEnumerable<ParticipantModel>> GetAllAsync();
        Task UpdateScoreAsync(int participantId, int score);
        Task DeleteAsync(int id);

        Task AddAsync(ParticipantModel model);
    }
}