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
    public class ResponseService : IResponseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ResponseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<bool> CreateResponse(QuestionModel questionModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteResponse(int id)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Pagination<ResponseModel>> GetResponses(PaginationModel paginationModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateResponse(QuestionModel questionModel)
        {
            throw new NotImplementedException();
        }
    }
}
