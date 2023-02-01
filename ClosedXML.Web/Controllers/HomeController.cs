using ClosedXML.Web.Extensions;
using ClosedXML.Web.Models;
using ClosedXML.Web.Models.DTO;
using ClosedXML.Web.Models.RequestModel;
using ClosedXML.Web.Models.ResponseModel;
using ClosedXML.Web.Models.ViewModel;
using ClosedXML.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClosedXML.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Fields
        private readonly ILogger<HomeController> _logger;
        private readonly ICovidService _covidService;
        private int DefaultPageSize { get; set; }
        #endregion

        #region Ctor
        public HomeController(ILogger<HomeController> logger,
            ICovidService covidService)
        {
            _logger = logger;
            _covidService = covidService;
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/home/getCovidListExcelAsync")]      
        public async Task<IActionResult> GetCovidListExcelAsync(DataTablesRequest request)
        {
            var dict = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());

            string column = "Id";
            string dir = "desc";
            if (request.Order != null)
            {
                var order = request.Order.FirstOrDefault();
                column = request.Columns.ToArray()[order.Column].Name;
                dir = order.Dir;
            }
            DefaultPageSize = request.Length;

            DateTime.TryParse(dict["covidDate"], out DateTime covidDate);
           

            CovidListRequestModel requestModel = new CovidListRequestModel
            {
                OrderColumn = column,
                PageIndex = request.Start,
                PageSize = request.Length,
                Orderby = dir,    
                City = dict.ContainsKey("city") && !string.IsNullOrEmpty(dict["city"].Trim()) ? int.Parse(dict["city"]) : null,
                Count = dict.ContainsKey("count") && !string.IsNullOrEmpty(dict["count"].Trim()) ? int.Parse(dict["count"]) : null,
                CovidDate = covidDate == DateTime.MinValue ? null : covidDate,
            };

            CovidListResponseModel res = await _covidService.GetByPage(requestModel);
            _DataTablesResponse<CovidListDto> result = new _DataTablesResponse<CovidListDto>();
            var columnNames = request.Columns.Select(k => k.Name).ToList();

            var dataTable = GeneralExtentions.GenerateDataTable<CovidListDto>(columnNames, res.payload.data);
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "CovidList.xlsx";
            byte[] file = ConvertToExcel.DataTableToExcel(dataTable);
            return new JsonResult(new FileDownloadModel { FileName = fileName, Base64File = Convert.ToBase64String(file), ContentType = contentType });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}