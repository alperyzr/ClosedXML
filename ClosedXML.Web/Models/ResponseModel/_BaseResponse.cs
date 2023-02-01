using Newtonsoft.Json;

namespace ClosedXML.Web.Models.ResponseModel
{
    [JsonObject(Title = "response")]
    public class _BaseResponse<T> where T : new()
    {
        [JsonProperty("status")]
        public int status { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }

        [JsonProperty("code")]
        public string code { get; set; }

        [JsonProperty("payload")]
        public T payload { get; set; }

        public _BaseResponse(T dto)
        {
            // this.IsSucces = true;
            this.code = "200";
            this.message = string.Empty;
            this.payload = dto;
        }

        public _BaseResponse(string message)
        {
            //  this.IsSucces = false;
            this.message = message;
            this.code = "400";
        }

        public _BaseResponse(T dto, string message)
        {

            //   this.IsSucces = true;
            this.message = message;
            this.code = "200";
            this.payload = dto;
        }

        public _BaseResponse(T dto, string message, bool IsSucces)
        {
            //  this.IsSucces = IsSucces;
            this.message = message;
            this.code = "200";
            this.payload = dto;
        }

        public _BaseResponse(T dto, string message, bool IsSucces, string ResultCode)
        {
            //   this.IsSucces = IsSucces;
            this.message = message;
            this.code = ResultCode;
            this.payload = dto;
        }
    }
}
