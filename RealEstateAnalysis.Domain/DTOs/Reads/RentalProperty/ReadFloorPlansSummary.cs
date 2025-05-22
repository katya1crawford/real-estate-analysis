using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadFloorPlansSummary
    {
        public ReadFloorPlansSummary(List<ReadFloorPlan> floorPlans)
        {
            FloorPlans = floorPlans.OrderByDescending(x => x.Units).ToList();
            Units = floorPlans.Sum(x => x.Units);
            AverageContractRent = Math.Round(Divide(floorPlans.Sum(x => x.AverageContractRent), floorPlans.Count(x => x.AverageContractRent > 0)), 0);
            AverageMarketRent = Math.Round(Divide(floorPlans.Sum(x => x.AverageMarketRent), floorPlans.Count(x => x.AverageMarketRent > 0)), 0);
            PercentOfMarketRent = Math.Round(Divide(floorPlans.Sum(x => x.PercentOfMarketRent), floorPlans.Count(x => x.PercentOfMarketRent > 0)), 2);
            AverageSquareFootage = (int)Math.Round(Divide(floorPlans.Sum(x => x.AverageSquareFootage), floorPlans.Count), 0);
        }

        public List<ReadFloorPlan> FloorPlans { get; }

        public int Units { get; }

        public decimal AverageContractRent { get; }

        public decimal AverageMarketRent { get; }

        public int AverageSquareFootage { get; }

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