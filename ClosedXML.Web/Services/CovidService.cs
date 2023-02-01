using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ClosedXML.Web.Helper;
using ClosedXML.Web.Models.DTO;
using ClosedXML.Web.Models.RequestModel;
using ClosedXML.Web.Models.ResponseModel;
using ClosedXML.Web.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using static ClosedXML.Web.Enums.ApplicationEnum;

namespace ClosedXML.Web.Services
{
    public class CovidService : ICovidService
    {
        #region Fiels
        private readonly IConfiguration _configuration;
        private string ApiUrl => _configuration["WebHost:ApiUrl"];
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        #endregion

        #region Ctor
        public CovidService(IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }
        #endregion

        #region Methods
        public async Task<CovidListResponseModel> GetByPage(CovidListRequestModel requestModel)
        {
            var result = await HttpPostHelper<PagedResultBase<CovidListDto>>.PostDataAsync(_httpContextAccessor, _httpClientFactory, $"{ApiUrl}/covid/getCovidListByFilter", requestModel);
            CovidListResponseModel res = new CovidListResponseModel();

            res.payload = new _BasePagingResponse<List<CovidListDto>>();

            res.payload.data = result.Payload.Items.ToList();
            res.payload.FilteredRows = result.Payload.RowCount;
            res.payload.TotalRows = result.Payload.RowCount;

            res.status = (int)CompletionStatus.Success;
            res.code = result.Code;
            res.message = result.Message;

            return res;
        }

        #endregion
    }
}
