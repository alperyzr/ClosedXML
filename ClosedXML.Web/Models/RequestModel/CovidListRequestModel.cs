using Newtonsoft.Json;

namespace ClosedXML.Web.Models.RequestModel
{
    public class CovidListRequestModel
    {
        [JsonProperty("page_size")]
        public int PageSize { get; set; }

        [JsonProperty("page_index")]
        public int PageIndex { get; set; }

        [JsonProperty("order_column")]
        public string OrderColumn { get; set; }

        [JsonProperty("order_by")]
        public string Orderby { get; set; }

        [JsonProperty("count")]
        public int? Count { get; set; }

        [JsonProperty("covid_date")]
        public DateTime? CovidDate { get; set; }

        [JsonProperty("city")]
        public int? City { get; set; }
    }
}
