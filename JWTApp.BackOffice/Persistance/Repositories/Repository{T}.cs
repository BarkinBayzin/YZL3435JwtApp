using JWTApp.BackOffice.Core.Application.Interfaces;
using JWTApp.BackOffice.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JWTApp.BackOffice.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly JWTContext _jwtContext;

        public Repository(JWTContext jwtContext)
        {
            _jwtContext = jwtContext;
        }

        public async Task CreateAsync(T entity)
        {
            await _jwtContext.Set<T>().AddAsync(entity);
            await _jwtContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync() => await _jwtContext.Set<T>().ToListAsync();

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _jwtContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public async Task<T> GetByIdAsync(object id) => await _jwtContext.Set<T>().FindAsync(id);

        public async Task RemoveAsync(T entity)
        {
            _jwtContext.Set<T>().Remove(entity);
            await _jwtContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _jwtContext.Set<T>().Update(entity);
            await _jwtContext.SaveChangesAsync();
        }
    }
}
