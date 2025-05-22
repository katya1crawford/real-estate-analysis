namespace RealEstateAnalysis.Domain.DTOs.Writes
{
    public class WriteUnitGroup
    {
        public long Id { get; set; }

        public double Bathrooms { get; set; }

        public int Bedrooms { get; set; }

        public int NumberOfUnits { get; set; }

        public int SquareFootage { get; set; }
    }
}