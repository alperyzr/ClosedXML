using ClosedXML.API.Extensions;
using ClosedXML.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClosedXML.API.Repositories
{
    public class _Repository<T> : _IRepository<T> where T : class
    {
        public readonly DbContext _dbContext;
        public readonly DbSet<T> _dbSet;

        public _Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }


        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Task<PagedResult<T>> FindAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, string orderBy, string orderColumn)
        {
            var query = _dbSet.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            var result = query.OrderByDynamic(orderColumn, (orderBy == "asc" ? false : true))
                                .GetPaged(pageIndex, pageSize);
            return Task.FromResult(result);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(T entity)
        {
            _dbSet.Remove(entity);
            var effectedRowCount = await _dbContext.SaveChangesAsync();
            return effectedRowCount > 0;
        }

        public async Task<int> Count()
        {
            int count = await _dbSet.CountAsync().ConfigureAwait(false);
            return count;
        }

        public async Task<T> OrderByThenFirstOrDefaultAsyc(Expression<Func<T, bool>> predicate, string orderBy, string orderColumn)
        {
            var query = await _dbSet.OrderByDynamic(orderColumn, (orderBy == "asc" ? false : true)).FirstOrDefaultAsync(predicate);
            return query;
        }
    }
}
