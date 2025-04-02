using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using PRN222.Kahoot.Repository.Enums;
using PRN222.Kahoot.Repository.Models;
using PRN222.Kahoot.Repository.UnitOfWork;
using PRN222.Kahoot.Service.BusinessModels;
using PRN222.Kahoot.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Kahoot.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateUser(UserModel userModel)
        {
            try
            {
                var user = _mapper.Map<User>(userModel);
                user.CreatedAt = DateTime.UtcNow.AddHours(7);
                user.Role = RoleEnum.Student.ToString();

                await _unitOfWork.UserRepository.AddAsync(user);
                var result = await _unitOfWork.SaveChangeAsync();
                if (result > 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.FindAsync(c => c.UserId == id);
                if (user != null)
                {
                    await _unitOfWork.UserRepository.DeletedAsync(user);
                    await _unitOfWork.SaveChangeAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserModel> GetById(int id)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.FindAsync(c => c.UserId == id);

                if (user == null)
                {
                    return null;
                }

                var userModel = _mapper.Map<UserModel>(user);

                return userModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Pagination<UserModel>> GetUsers(PaginationModel paginationModel, SortUser sort)
        {
            try
            {
                var users = await _unitOfWork.UserRepository.GetAsync(c => (c.Role.Equals(sort.Role) || sort.Role == null),
                                c => sort.Username switch
                                {
                                    "asc" => c.OrderBy(x => x.Username),
                                    "desc" => c.OrderByDescending(x => x.Username),
                                    _ => c.OrderBy(x => x.UserId)
                                });

                var list = users.Skip((paginationModel.PageIndex - 1) * paginationModel.PageSize)
                    .Take(paginationModel.PageSize)
                    .ToList();

                var userModels = _mapper.Map<List<UserModel>>(list);

                return new Pagination<UserModel>(userModels, users.Count());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserModel> Login(string username, string password)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.FindAsync(c => c.Username == username && c.Password == password);

                if (user == null)
                {
                    return null;
                }

                var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

                var claimsIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(10)
                };

                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext != null)
                {
                    await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authProperties);
                }

                var userModel = _mapper.Map<UserModel>(user);

                return userModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateUser(UserModel userModel)
        {
            try
            {
                var user = _mapper.Map<User>(userModel);

                await _unitOfWork.UserRepository.UpdateAsync(user);
                var result = await _unitOfWork.SaveChangeAsync();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Logout()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if ( httpContext != null)
            {
                await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
