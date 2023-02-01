using ClosedXML.API.Models.RequestModel;
using ClosedXML.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ClosedXML.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CovidController : ControllerBase
    {
        #region Fields
        private readonly ICovidService _covidService;
        #endregion

        #region Ctor
        public CovidController(ICovidService covidService)
        {
            _covidService= covidService;
        }
        #endregion

        #region Methods
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("/covid/getCovidListByFilter")]
        // [CustomActionFilter(Permissions.GetDebitAuthLogListByFilter)]
        public async Task<IActionResult> GetDebitAuthLogListByFilter([FromBody] CovidListRequestModel request)
        {
            var result = await _covidService.GetCovidListByFilter(request);                          
            return Ok(result);
        }

        #endregion
    }
}
