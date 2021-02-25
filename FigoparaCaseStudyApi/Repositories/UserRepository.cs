using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FigoparaCaseStudyApi.Entities.Db;
using FigoparaCaseStudyApi.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FigoparaCaseStudyApi.Repositories
{
    public interface IUserRepository
    {
        Task<bool> HasUser(Expression<Func<User, bool>> who);
        EntityEntry<User> Add(User user);

        Task Update(User entity);
        Task<int> Save();
        IQueryable<User> Queryable();

        Task Delete(User user);

        Task<User> Find(Expression<Func<User, bool>> expression);
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext DbContext;

        public UserRepository(UserDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="who"></param>
        /// <returns></returns>
        public Task<bool> HasUser(Expression<Func<User, bool>> who)
        {
            return DbContext.Users.AnyAsync(who);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public EntityEntry<User> Add(User user)
        {
            return DbContext.Users.Add(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task Update(User entity)
        {
            DbContext.Attach(entity);

            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task Delete(User user)
        {
            DbContext.Users.Remove(user);

            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<int> Save()
        {
            return DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> Queryable()
        {
            return DbContext.Users;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<User> Find(Expression<Func<User, bool>> expression)
        {
            return DbContext.Users.FirstOrDefaultAsync(expression);
        }
    }
}