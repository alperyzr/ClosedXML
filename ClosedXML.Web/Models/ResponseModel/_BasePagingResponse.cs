using Newtonsoft.Json;

namespace ClosedXML.Web.Models.ResponseModel
{
    public class _BasePagingResponse<T>
    {
        [JsonProperty("total_rows")]
        public int TotalRows { get; set; }

        [JsonProperty("filtered_rows")]
        public int FilteredRows { get; set; }

        [JsonProperty("results")]
        public T data { get; set; }

        public _BasePagingResponse()
        {
            TotalRows = 1;
            FilteredRows = 1;
        }

        public _BasePagingResponse(T dto)
        {
            this.data = dto;
            TotalRows = 1;
            FilteredRows = 1;
        }

        public _BasePagingResponse(T dto, int totalRows)
        {
            this.data = dto;
            TotalRows = totalRows;
            FilteredRows = totalRows;
        }

        public _BasePagingResponse(T dto, int totalRows, int filteredRows)
        {
            this.data = dto;
            TotalRows = totalRows;
            FilteredRows = filteredRows;
        }
    }
}
