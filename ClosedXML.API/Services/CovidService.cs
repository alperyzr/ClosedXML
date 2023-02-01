using ClosedXML.API.Extensions;
using ClosedXML.API.Models.DTO;
using ClosedXML.API.Models.RequestModel;
using ClosedXML.API.Models.ResponseModel;
using ClosedXML.API.Services.Interfaces;
using ClosedXML.API.UnitOfWorks;

namespace ClosedXML.API.Services
{
    public class CovidService : ICovidService
    {
        public readonly IUnitOfWork _unitOfWork;
        public CovidService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceResult<PagedResult<CovidListItem>>> GetCovidListByFilter(CovidListRequestModel requestModel)
        {
            var items = await _unitOfWork.Covids.GetByFilter(requestModel);
            var res = new PagedResult<CovidListItem>
            {
                CurrentIndex = items.CurrentIndex,
                OrderBy = requestModel.Orderby,
                OrderColumn = requestModel.OrderColumn,
                PageCount = items.PageCount,
                PageSize = items.PageSize,
                RowCount = items.RowCount,
                Items = items.Items.Select(x => new CovidListItem
                {
                    Id = x.Id,
                    City= x.City,
                    CovidDate= x.CovidDate,
                    Count = x.Count
                }).ToList()
            };

            return ServiceResult<PagedResult<CovidListItem>>.SuccessResult(res);
        }
    }
}
