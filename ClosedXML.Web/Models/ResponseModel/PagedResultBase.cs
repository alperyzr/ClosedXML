using System.Text.Json.Serialization;

namespace ClosedXML.Web.Models.ResponseModel
{
    public class PagedResultBase<T> where T : class
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("payload")]
        public PagedResult<T> Payload { get; set; }
    }

    public class PagedResult<T> where T : class
    {
        [JsonPropertyName("currentIndex")]
        public int CurrentIndex { get; set; }

        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("rowCount")]
        public int RowCount { get; set; }

        [JsonPropertyName("orderColumn")]
        public string OrderColumn { get; set; }

        [JsonPropertyName("orderBy")]
        public string OrderBy { get; set; }

        [JsonPropertyName("items")]
        public IList<T> Items { get; set; }

        public PagedResult()
        {
            Items = new List<T>();
        }
    }
}
