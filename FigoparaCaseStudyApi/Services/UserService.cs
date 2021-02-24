using System;
using System.Threading.Tasks;
using FigoparaCaseStudyApi.Modules;
using FigoparaCaseStudyApi.Repositories;
using FigoparaCaseStudyApi.Request;
using FigoparaCaseStudyApi.Response;
using Microsoft.Extensions.Logging;

namespace FigoparaCaseStudyApi.Services
{
    public interface IUserService
    {
        public Task<ServiceResponse> Add(UserAddRequest request);

        public Task<ServiceResponse> Delete(UserDeleteRequest request);
        public Task<ServiceResponse> Get(UserGetRequest request);
        public Task<ServiceResponse> Update(UserAddRequest request);
        public Task<ServiceResponse> Search(UserSearchRequest request);
    }

    public class UserService : IUserService
    {
        private readonly ILogger<UserService> Logger;
        private readonly IUserRepository UserRepository;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            Logger         = logger;
            UserRepository = userRepository;
        }

        public async Task<ServiceResponse> Add(UserAddRequest request)
        {
            ServiceResponse Response = new ServiceResponse();
            try
            {
                var user = new User
                {
                    Email    = request.Email,
                    Name     = request.Name,
                    Password = request.Password,
                    Phone    = request.Phone,
                    Surname  = request.Surname
                };

                var hasUser =await UserRepository.HasUser(what => what.Name == user.Name 
                                                          || what.Surname == user.Surname 
                                                          || what.Phone == user.Phone 
                                                          || what.Email == user.Email);

                if (hasUser)
                {
                    Response.Status  = false;
                    Response.Data    = user;
                    Response.Message = $"{user.Name} {user.Surname} kullanıcısı kayıtlı";
                }

                UserRepository.Add(user);

                UserRepository.Save();
            }
            catch (Exception ex)
            {
                Response.Status  = false;
                Response.Message = "işlem sırasında bir hata alındı.";
                Response.Data = new
                {
                    Code = 500,
                };
                Logger.LogError(ex.Message, ex);
            }

            return await Task.FromResult(Response);
        }

        public Task<ServiceResponse> Delete(UserDeleteRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse> Get(UserGetRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse> Update(UserAddRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse> Search(UserSearchRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}