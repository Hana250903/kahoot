using PRN222.Kahoot.Service.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.Services.Interfaces
{
    public interface IUserService
    {
        Task<Pagination<UserModel>> GetUsers(PaginationModel paginationModel, SortUser sort);
        Task<UserModel> GetById(int id);
        Task<bool> CreateUser(UserModel userModel);
        Task<bool> UpdateUser(UserModel userModel);
        Task<bool> DeleteUser(int id);

        Task<UserModel> Login(string username, string password);

        Task Logout();
    }
}
