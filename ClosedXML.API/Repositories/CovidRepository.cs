using ClosedXML.API.Entities;
using ClosedXML.API.Extensions;
using ClosedXML.API.Models.RequestModel;
using ClosedXML.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClosedXML.API.Repositories
{
    public class CovidRepository : _Repository<Covid>, ICovidRepository
    {
        public CovidRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedResult<Covid>> GetByFilter(CovidListRequestModel requestModel)
        {
            var query = _dbSet.AsQueryable();

            if (requestModel.City != 0)
                query = query.Where(x => x.City == requestModel.City);

                query = query.Where(x => x.Count == requestModel.Count);

            if (requestModel.CovidDate != DateTime.MinValue)
                query = query.Where(x => x.CovidDate == requestModel.CovidDate);
          
            var result = query.OrderByDynamic(requestModel.OrderColumn, (requestModel.Orderby == "asc" ? false : true))
                               .GetPaged(requestModel.PageIndex, requestModel.PageSize);

            return result;
        }

       
    }
}
