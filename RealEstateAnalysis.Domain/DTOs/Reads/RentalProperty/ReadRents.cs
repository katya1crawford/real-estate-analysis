using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadRents
    {
        public ReadRents(List<ReadRentsFloorPlan> rentsFloorPlans)
        {
            RentsFloorPlans = rentsFloorPlans.OrderByDescending(x => x.Units).ToList();
            Units = rentsFloorPlans.Sum(x => x.Units);
            AverageContractRent = Math.Round(Divide(rentsFloorPlans.Sum(x => x.AverageContractRent), rentsFloorPlans.Count()), 0);
            AverageMarketRent = Math.Round(Divide(rentsFloorPlans.Sum(x => x.AverageMarketRent), rentsFloorPlans.Count(x => x.AverageMarketRent > 0)), 0);
            PercentOfMarketRent = Math.Round(Divide(rentsFloorPlans.Sum(x => x.PercentOfMarketRent), rentsFloorPlans.Count(x => x.PercentOfMarketRent > 0)), 2);
        }

        public List<ReadRentsFloorPlan> RentsFloorPlans { get; }

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