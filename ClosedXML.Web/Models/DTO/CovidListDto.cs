using ClosedXML.Web.Helper.Attributes;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text.Json.Serialization;

namespace ClosedXML.Web.Models.DTO
{
    public class CovidListDto
    {
        [ExcelHeader(Name = "Id")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [ExcelHeader(Name = "Şehir")]
        [JsonPropertyName("city")]
        public int City { get; set; }

        [ExcelHeader(Name = "Covid Tarihi")]
        [JsonPropertyName("covidDate")]     
        public DateTime? CovidDate { get; set; }

        [ExcelHeader(Name = "Sayı")]
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
