using System;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadFloorPlan
    {
        public ReadFloorPlan(string floorPlan, int units, int bedrooms, double bathrooms, int averageSquareFootage, decimal averageContractRent, decimal minimumContractRent,
            decimal maximumContractRent, decimal averageMarketRent, decimal minimumMarketRent, decimal maximumMarketRent)
        {
            FloorPlan = floorPlan;
            Units = units;
            Bedrooms = bedrooms;
            Bathrooms = bathrooms;
            AverageSquareFootage = averageSquareFootage;
            AverageContractRent = averageContractRent;
            AverageMarketRent = averageMarketRent;
            ContractRentRange = minimumContractRent - maximumContractRent == 0
                ? minimumContractRent.ToString("c")
                : $"{minimumContractRent:c} - {maximumContractRent:c}";
            MarketRentRange = minimumMarketRent - maximumMarketRent == 0
                ? minimumMarketRent.ToString("c")
                : $"{minimumMarketRent:c} - {maximumMarketRent:c}";
            PercentOfMarketRent = Math.Round(Divide(averageContractRent, averageMarketRent) * 100, 2);
        }

        public string FloorPlan { get; }

        public int Units { get; }

        public int Bedrooms { get; }

        public double Bathrooms { get; }

        public int AverageSquareFootage { get; }

        public decimal AverageContractRent { get; }

        public decimal AverageMarketRent { get; }

        public string ContractRentRange { get; }

        public string MarketRentRange { get; }

        public decimal PercentOfMarketRent { get; }

        private decimal Divide(decimal numerator, decimal denominator)
        {
            if (denominator == 0)
            {
                return 0;
            }

            return numerator / denominator;
        }
    }
}