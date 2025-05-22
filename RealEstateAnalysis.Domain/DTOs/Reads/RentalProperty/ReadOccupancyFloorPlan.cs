using System;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadOccupancyFloorPlan
    {
        public ReadOccupancyFloorPlan(string floorPlan, int occupiedUnits, int vacantUnits)
        {
            FloorPlan = floorPlan;
            Units = occupiedUnits + vacantUnits;
            OccupiedUnits = occupiedUnits;
            VacantUnits = vacantUnits;
            PercentOfVacantUnits = Math.Round(Divide(VacantUnits, Units) * 100, 2);
        }

        public string FloorPlan { get; }

        public int Units { get; }

        public int OccupiedUnits { get; }

        public int VacantUnits { get; }

        public decimal PercentOfVacantUnits { get; }

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