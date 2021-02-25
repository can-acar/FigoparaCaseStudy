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
        Task<ServiceResponse> Add(UserAddRequest request);

        Task<ServiceResponse> Delete(UserDeleteRequest request);
        Task<ServiceResponse> Get(UserGetRequest request);
        Task<ServiceResponse> Update(UserUpdateRequest request);

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
            ServiceResponse response = new ServiceResponse();
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

                var hasUser = await UserRepository.HasUser(what => what.Name == request.Name || what.Surname == request.Surname || what.Phone == request.Phone || what.Email == request.Email);

                if (hasUser)
                {
                    response.Status  = false;
                    response.Data    = request;
                    response.Message = $"{request.Name} {request.Surname} kullanıcısı kayıtlı";

                    return await Task.FromResult(response);
                }

                UserRepository.Add(user);

                if (await UserRepository.Save() != -1)
                {
                    Logger.LogInformation("@user Kayıt işlemi Yapıldı", user);

                    response.Status = true;

                    response.Data = user;

                    response.Message = "Kayıt işlemi Yapıldı";
                }
                else
                {
                    Logger.LogError("Kayıt işlemi Yapılamadı", user);

                    response.Status = false;

                    response.Message = "işlem sırasında bir hata alındı.";
                }
            }
            catch (Exception ex)
            {
                response.Status  = false;
                response.Message = "işlem sırasında bir hata alındı.";

                Logger.LogError(ex.Message, ex);
            }

            return await Task.FromResult(response);
        }

        public async Task<ServiceResponse> Delete(UserDeleteRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse> Get(UserGetRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse> Update(UserUpdateRequest request)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var user = new User
                {
                    Id       = request.Id,
                    Email    = request.Email,
                    Name     = request.Name,
                    Password = request.Password,
                    Phone    = request.Phone,
                    Surname  = request.Surname
                };


                UserRepository.Add(user);

                if (await UserRepository.Save() != -1)
                {
                    Logger.LogInformation("@user Kayıt işlemi Yapıldı", user);

                    response.Status = true;

                    response.Data = user;

                    response.Message = "Kayıt işlemi Yapıldı";
                }
                else
                {
                    Logger.LogError("Kayıt işlemi Yapılamadı", user);

                    response.Status = false;

                    response.Message = "işlem sırasında bir hata alındı.";
                }
            }
            catch (Exception ex)
            {
                response.Status  = false;
                response.Message = "işlem sırasında bir hata alındı.";

                Logger.LogError(ex.Message, ex);
            }

            return await Task.FromResult(response);
        }
    }
}