using System;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadRentsFloorPlan
    {
        public ReadRentsFloorPlan(string floorPlan, int units, decimal averageContractRent, decimal averageMarketRent)
        {
            FloorPlan = floorPlan;
            Units = units;
            AverageContractRent = averageContractRent;
            AverageMarketRent = averageMarketRent;
            PercentOfMarketRent = Math.Round(Divide(averageContractRent, averageMarketRent) * 100, 2);
        }

        public string FloorPlan { get; }

        public int Units { get; }

        public decimal AverageContractRent { get; }

        public decimal AverageMarketRent { get; }

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