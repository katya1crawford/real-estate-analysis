using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadOccupancy
    {
        public ReadOccupancy(List<ReadOccupancyFloorPlan> occupancyFloorPlans)
        {
            OccupancyFloorPlans = occupancyFloorPlans;
            TotalUnits = occupancyFloorPlans.Sum(x => x.Units);
            TotalOccupiedUnits = occupancyFloorPlans.Sum(x => x.OccupiedUnits);
            TotalVacantUnits = occupancyFloorPlans.Sum(x => x.VacantUnits);
            TotalPercentOfVacantUnits = Math.Round(Divide(TotalVacantUnits, TotalUnits) * 100, 2);
        }

        public List<ReadOccupancyFloorPlan> OccupancyFloorPlans { get; }

        public int TotalUnits { get; }

        public int TotalOccupiedUnits { get; }

        public int TotalVacantUnits { get; }

        public decimal TotalPercentOfVacantUnits { get; }

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