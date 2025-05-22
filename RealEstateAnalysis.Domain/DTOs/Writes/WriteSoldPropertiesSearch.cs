namespace RealEstateAnalysis.Domain.DTOs.Writes
{
    public class WriteSoldPropertiesSearch
    {
        public long? PropertyTypeId { get; set; }

        public int? UnitsMin { get; set; }

        public int? UnitsMax { get; set; }

        public int? YearMin { get; set; }

        public int? YearMax { get; set; }

        public string ZipCode { get; set; }

        public int? ZipCodeRadiusInMiles { get; set; }

        public WriteAddress SubjectProperty { get; set; }
    }
}