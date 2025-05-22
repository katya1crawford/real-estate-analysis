using System.Collections.Generic;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadFloorPlanAverageContractRentByMonthGroup
    {
        public ReadFloorPlanAverageContractRentByMonthGroup(string floorPlan, List<ReadFloorPlanAverageContractRentByMonth> values)
        {
            FloorPlan = floorPlan;
            Items = values;
        }

        public string FloorPlan { get; }

        public List<ReadFloorPlanAverageContractRentByMonth> Items { get; }
    }
}