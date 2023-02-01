using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace ClosedXML.API.Models.RequestModel
{
    public class _BaseListRequestModel
    {
        [JsonPropertyName("page_size")]
        [Display(Name = "page_size")]
        [Required(ErrorMessage = "{0} alanı zorunlu alandır")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "{0} alanna sadece sayı değeri girilebilir")]
        [Range(minimum: 1, maximum: 1000, ErrorMessage = "{0} alanı {1} ve {2} aralığında değer almaktadır")]
        public int PageSize { get; set; }

        [JsonPropertyName("page_index")]
        [Display(Name = "page_index")]
        [Required(ErrorMessage = "{0} alanı zorunlu alandır")]
        [RegularExpression("([0-9]*)", ErrorMessage = "{0} sadece sayı değeri girilebilir")]
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "{0} alanı {1}'den büyük bir değer almaktadır")]
        public int PageIndex { get; set; }

        [JsonPropertyName("order_column")]
        [Display(Name = "order_column")]
        [Required(ErrorMessage = "{0} alanı zorunlu alandır")]
        [MinLength(2, ErrorMessage = "{0} alanı min {1} karakter içermelidir")]
        public string OrderColumn { get; set; }

        [JsonPropertyName("order_by")]
        [Display(Name = "order_by")]
        [Required(ErrorMessage = "{0} alanı zorunlu alandır")]
        [MinLength(3, ErrorMessage = "{0} alanı min {1} karakter içermelidir")]
        public string Orderby { get; set; }
    }
}
