using PRN222.Kahoot.Service.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.Services.Interfaces
{
    public interface IResponseService
    {
        Task<Pagination<ResponseModel>> GetResponses(PaginationModel paginationModel);
        Task<QuestionModel> GetById(int id);
        Task<bool> CreateResponse(QuestionModel questionModel);
        Task<bool> UpdateResponse(QuestionModel questionModel);
        Task<bool> DeleteResponse(int id);
    }
}
