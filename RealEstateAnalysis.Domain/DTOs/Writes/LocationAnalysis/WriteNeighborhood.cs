namespace RealEstateAnalysis.Domain.DTOs.Writes.LocationAnalysis
{
    public class WriteNeighborhood
    {
        public string NeighborhoodName { get; set; }

        public string City { get; set; }

        public decimal MedianHouseholdIncome { get; set; }

        public decimal MedianContractRent { get; set; }

        public decimal CityUnemploymentRate { get; set; }

        public decimal NeighborhoodUnemploymentRate { get; set; }

        public decimal PovertyRate { get; set; }

        public decimal EthnicMixLargestSlicePercent { get; set; }

        public int HomesMedianDaysOnMarket { get; set; }

        public long StateId { get; set; }
    }
}