namespace ClosedXML.API.Models.DTO
{
    public class CovidListItem
    {
        public int Id { get; set; }
        public int City { get; set; }
        public DateTime? CovidDate { get; set; }
        public int Count { get; set; }
    }
}
