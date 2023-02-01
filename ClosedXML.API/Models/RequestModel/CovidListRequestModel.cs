using System.Text.Json.Serialization;

namespace ClosedXML.API.Models.RequestModel
{
    public class CovidListRequestModel:_BaseListRequestModel
    {
        [JsonPropertyName("count")]
        public int? Count { get; set; }

        [JsonPropertyName("covid_date")]
        public DateTime? CovidDate { get; set; }

        [JsonPropertyName("city")]
        public int? City { get; set; }
    }
}
