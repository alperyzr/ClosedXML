using Newtonsoft.Json;

namespace ClosedXML.Web.Models.ResponseModel
{
    public class _DataTablesResponse<T> where T : class
    {
        [JsonProperty("draw")]
        public int Draw { get; set; }

        [JsonProperty("recordsTotal")]
        public int RecordsTotal { get; set; }

        [JsonProperty("recordsFiltered")]
        public int RecordsFiltered { get; set; }

        [JsonProperty("data")]
        public IEnumerable<T> Data { get; set; }
    }
}
