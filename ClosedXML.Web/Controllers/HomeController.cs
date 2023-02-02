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
       
        private int DefaultPageSize { get; set; }
        #endregion

        #region Ctor
        public HomeController(ILogger<HomeController> logger,
            ICovidService covidService)
        {
            _logger = logger;            
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
        }

       


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}