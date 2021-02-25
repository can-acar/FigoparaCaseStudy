using System;
using System.Linq;
using System.Threading.Tasks;
using FigoparaCaseStudyApi.Modules;
using FigoparaCaseStudyApi.Repositories;
using FigoparaCaseStudyApi.Request;
using FigoparaCaseStudyApi.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FigoparaCaseStudyApi.Services
{
    public interface IUserService
    {
        Task<ServiceResponse> Add(UserAddRequest request);

        Task<ServiceResponse> Delete(UserDeleteRequest request);
        Task<ServiceResponse> Get(UserGetRequest request);
        Task<ServiceResponse> Update(UserUpdateRequest request);

        Task<ServiceResponse> Searh(UserSearchRequest request);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> Delete(UserDeleteRequest request)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var user = await UserRepository.Find(what => what.Id == request.Id);

                if (user == null)
                {
                    response.Status  = false;
                    response.Data    = request;
                    response.Message = $"kullanıcı bulunamadı";

                    return await Task.FromResult(response);
                }

                UserRepository.Delete(user);

                if (await UserRepository.Save() != -1)
                {
                    Logger.LogInformation("{@user} silindi", user);

                    response.Status = true;

                    response.Data = user;

                    response.Message = $"{user.Name} silindi";
                }
                else
                {
                    Logger.LogError("{@user} silinemedi", user);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ServiceResponse> Get(UserGetRequest request)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var user = await UserRepository.Find(what => what.Id == request.Id);

                if (user == null)
                {
                    response.Status  = false;
                    response.Data    = request;
                    response.Message = $"kullanıcı bulunamadı";

                    return await Task.FromResult(response);
                }

                response.Status = true;
                response.Data   = user;
            }
            catch (Exception ex)
            {
                response.Status  = false;
                response.Message = "işlem sırasında bir hata alındı.";

                Logger.LogError(ex.Message, ex);
            }

            return await Task.FromResult(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> Update(UserUpdateRequest request)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var user = await UserRepository.Find(what => what.Id == request.Id);


                user.Id       = request.Id;
                user.Email    = request.Email;
                user.Name     = request.Name;
                user.Password = request.Password;
                user.Phone    = request.Phone;
                user.Surname  = request.Surname;


                UserRepository.Update(user);

                if (await UserRepository.Save() != -1)
                {
                    Logger.LogInformation("@{user} Güncelleme işlemi yapıldı", user);

                    response.Status = true;

                    response.Data = user;

                    response.Message = "G@{user} Güncelleme işlemi yapıldı";
                }
                else
                {
                    Logger.LogError("Güncelleme işlemi yapılamadı", user);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ServiceResponse> Searh(UserSearchRequest request)
        {
            ServiceResponse response = new ServiceResponse();
            try
            {
                var query = UserRepository.Queryable();


                if (!string.IsNullOrEmpty(request.Email))
                {
                    query = query.Where(p => EF.Functions.Like(p.Email.ToLower(), $"%{request.Email.ToLower()}%"));
                }

                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{request.Name.ToLower()}%"));
                }

                if (!string.IsNullOrEmpty(request.Surname))
                {
                    query = query.Where(p => EF.Functions.Like(p.Surname.ToLower(), $"%{request.Surname.ToLower()}%"));
                }

                if (!string.IsNullOrEmpty(request.Phone))
                {
                    query = query.Where(p => EF.Functions.Like(p.Phone.ToLower(), $"%{request.Phone.ToLower()}%"));
                }

                if (query.Any())
                {
                    response.Status = true;
                    response.Data   = query.OrderBy(order => order.Name).ToList();
                }
                else
                {
                    response.Status  = false;
                    response.Message = "Kayıt bulunamadı";
                }

                return await Task.FromResult(response);
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