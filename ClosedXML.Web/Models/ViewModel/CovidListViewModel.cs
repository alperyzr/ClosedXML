using ClosedXML.Web.Models.DTO;
using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClosedXML.Web.Models.ViewModel
{
    public class CovidListViewModel
    {
        public List<CovidListItem> items { get; set; }

        
        public CovidListViewModel()
        {
            items = new List<CovidListItem>();
        }
    }
}
