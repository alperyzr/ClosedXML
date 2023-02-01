using System.Linq.Expressions;

namespace ClosedXML.API.Extensions
{
    public abstract class PagedResultBase
    {
        public int CurrentIndex { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
        public string OrderColumn { get; set; }
        public string OrderBy { get; set; }
      
    }

    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Items { get; set; }

        public PagedResult()
        {
            Items = new List<T>();
        }
    }

    public static class DataPaginationExtensions
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int index, int pageSize) where T : class
        {

            int maxRowSize = 1000;

            var result = new PagedResult<T>();

            result.CurrentIndex = index;

            result.PageSize = pageSize > 100 && pageSize == 0 ? 100 : pageSize;

            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;

            result.PageCount = (int)Math.Ceiling(pageCount);

            if (pageSize > 0)
            {
                result.Items = query.Skip(index).Take(pageSize).ToList();
            }
            else
            {
                result.Items = query.Take(maxRowSize).ToList();
            }

            return result;
        }
    }

    public static class DataOrdered
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string sortColumn, bool descending)
        {
            // Dynamically creates a call like this: query.OrderBy(p =&gt; p.SortColumn)
            var parameter = Expression.Parameter(typeof(T), "p");

            string command = "OrderBy";

            if (descending)
            {
                command = "OrderByDescending";
            }

            Expression resultExpression = null;

            var property = typeof(T).GetProperty(sortColumn);
            // this is the part p.SortColumn

            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            // this is the part p =&gt; p.SortColumn
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            // finally, call the "OrderBy" / "OrderByDescending" method with the order by lamba expression
            resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { typeof(T), property.PropertyType },
               query.Expression, Expression.Quote(orderByExpression));

            return query.Provider.CreateQuery<T>(resultExpression);

        }
    }
}
