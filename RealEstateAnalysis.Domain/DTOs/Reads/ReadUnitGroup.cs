namespace RealEstateAnalysis.Domain.DTOs.Reads
{
    public class ReadUnitGroup
    {
        public ReadUnitGroup(long id, double bathrooms, int bedrooms, int numberOfUnits, int squareFootage)
        {
            Id = id;
            Bathrooms = bathrooms;
            Bedrooms = bedrooms;
            NumberOfUnits = numberOfUnits;
            SquareFootage = squareFootage;
        }

        public long Id { get; }

        public double Bathrooms { get; }

        public int Bedrooms { get; }

        public int NumberOfUnits { get; }

        public int SquareFootage { get; }
    }
}