using System.Text.Json.Serialization;
using static ClosedXML.API.Enums.ApplicationEnums;

namespace ClosedXML.API.Models.ResponseModel
{
    public class ServiceResult<T>
    {

        public CompletionStatus Status { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("payload")]
        public T Payload { get; set; }

        public ServiceResult(CompletionStatus status, T payload, string message, string code)
        {
            Status = status;
            Payload = payload;
            Message = message;
            Code = code;
        }

        // public ServiceResult(CompletionStatus status, T payload, string message, string code)
        // {
        //     Status = status;
        //     Payload = payload;
        //     Message = message;
        //     Code = code;
        // }

        //public static ServiceResult<T> SuccessResult(T payload, string message = "Ok", string code = "200")
        //{
        //    return new ServiceResult<T>(CompletionStatus.Success, payload, message, code);
        //}

        public static ServiceResult<T> SuccessResult(T payload, string message = "Ok", string code = "200")
        {
            return new ServiceResult<T>(CompletionStatus.Success, payload, message, code);
        }

        public static ServiceResult<T> WarningResult(T payload, string message = "Warning", string code = "400")
        {
            return new ServiceResult<T>(CompletionStatus.Warning, payload, message, code);
        }

        public static ServiceResult<T> ErrorResult(T payload, string message = "Error", string code = "500")
        {
            return new ServiceResult<T>(CompletionStatus.Failed, payload, message, code);
        }

        // public static ServiceResult<T> ErrorResult(T payload, string message, string code)
        // {
        //     return new ServiceResult<T>(CompletionStatus.Failed, payload, message, code);
        // }
    }
}
