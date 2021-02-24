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
        Task<int> Save();
    }

    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext DbContext;

        public UserRepository(UserDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<bool> HasUser(Expression<Func<User, bool>> who)
        {
            return DbContext.Users.AnyAsync(who);
        }

        public EntityEntry<User> Add(User user)
        {
            return DbContext.Users.Add(user);
        }

        public void Delete(User user)
        {
            DbContext.Users.Remove(user);
        }

        public Task<int> Save()
        {
            return DbContext.SaveChangesAsync();
        }
    }
}