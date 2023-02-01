using ClosedXML.API.Extensions;
using ClosedXML.API.Models.DTO;
using ClosedXML.API.Models.RequestModel;
using ClosedXML.API.Models.ResponseModel;

namespace ClosedXML.API.Services.Interfaces
{
    public interface ICovidService
    {
        Task<ServiceResult<PagedResult<CovidListItem>>> GetCovidListByFilter(CovidListRequestModel requestModel);
    }
}
