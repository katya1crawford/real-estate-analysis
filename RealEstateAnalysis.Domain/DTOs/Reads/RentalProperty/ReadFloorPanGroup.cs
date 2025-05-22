using System.Collections.Generic;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadFloorPanGroup
    {
        public ReadFloorPanGroup(string floorPlan, List<ReadRentRollItem> rentRollItems)
        {
            FloorPlan = floorPlan;
            RentRollItems = rentRollItems;
        }

        public string FloorPlan { get; }

        public List<ReadRentRollItem> RentRollItems { get; }
    }
}