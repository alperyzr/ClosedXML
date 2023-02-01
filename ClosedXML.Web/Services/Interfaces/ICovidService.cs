using ClosedXML.Web.Models.DTO;
using ClosedXML.Web.Models.RequestModel;
using ClosedXML.Web.Models.ResponseModel;

namespace ClosedXML.Web.Services.Interfaces
{
    public interface ICovidService
    {
        Task<CovidListResponseModel> GetByPage(CovidListRequestModel requestModel);
    }
}
