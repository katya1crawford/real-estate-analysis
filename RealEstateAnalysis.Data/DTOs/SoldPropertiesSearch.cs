namespace RealEstateAnalysis.Data.DTOs
{
    public class SoldPropertiesSearch
    {
        public string Address { get; set; }

        public string City { get; set; }

        public long? PropertyTypeId { get; set; }

        public long? StateId { get; set; }

        public int? UnitsMax { get; set; }

        public int? UnitsMin { get; set; }

        public int? YearMax { get; set; }

        public int? YearMin { get; set; }

        public List<string> ZipCodes { get; set; }
    }
}