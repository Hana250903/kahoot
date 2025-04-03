using PRN222.Kahoot.Service.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.Services.Interfaces
{
    public interface ISessionService
    {
        Task<SessionModel> GetSessionByCodeAsync(string sessionCode);
    }
}
