namespace RealEstateAnalysis.Domain.DTOs.Reads.LocationAnalysis
{
    public class ReadDecimalRange
    {
        public ReadDecimalRange(decimal from, decimal to)
        {
            From = from;
            To = to;
        }

        public decimal From { get; }

        public decimal To { get; }
    }
}