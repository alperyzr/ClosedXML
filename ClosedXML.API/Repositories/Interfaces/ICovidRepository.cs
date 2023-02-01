using ClosedXML.API.Entities;
using ClosedXML.API.Extensions;
using ClosedXML.API.Models.RequestModel;

namespace ClosedXML.API.Repositories.Interfaces
{
    public interface ICovidRepository : _IRepository<Covid>
    {
        Task<PagedResult<Covid>> GetByFilter(CovidListRequestModel requestModel);
    }
}
