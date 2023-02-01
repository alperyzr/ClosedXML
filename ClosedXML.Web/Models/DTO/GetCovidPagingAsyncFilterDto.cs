namespace ClosedXML.Web.Models.DTO
{
    public class GetCovidPagingAsyncFilterDto
    {
        public int Id { get; set; }

        public int City { get; set; }

        public DateTime? CovidDate { get; set; }

        public int Count { get; set; }
    }
}
