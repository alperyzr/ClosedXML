using ClosedXML.API.Extensions;
using System.Linq.Expressions;

namespace ClosedXML.API.Repositories.Interfaces
{
    public interface _IRepository<T> where T : class
    {
        public Task<PagedResult<T>> FindAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize, string orderBy, string orderColumn);
        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> Delete(T entity);
        Task<int> Count();
        public Task<T> OrderByThenFirstOrDefaultAsyc(Expression<Func<T, bool>> predicate, string orderBy, string orderColumn);
    }
}
