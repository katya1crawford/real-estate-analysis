namespace RealEstateAnalysis.Domain.DTOs.Writes
{
    public class WriteAddress
    {
        public string Address { get; set; }

        public string City { get; set; }

        public long StateId { get; set; }

        public string ZipCode { get; set; }
    }
}