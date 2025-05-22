using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadFloorPlanAverageContractRentByMonthSummary
    {
        public ReadFloorPlanAverageContractRentByMonthSummary(List<ReadFloorPlanAverageContractRentByMonthGroup> floorPlanAverageContractRentByMonthGroups)
        {
            Categories = BuildCategories(floorPlanAverageContractRentByMonthGroups);
            FloorPlanAverageContractRentByMonthGroups = floorPlanAverageContractRentByMonthGroups;
        }

        public List<string> Categories { get; }

        public List<ReadFloorPlanAverageContractRentByMonthGroup> FloorPlanAverageContractRentByMonthGroups { get; }

        private List<string> BuildCategories(List<ReadFloorPlanAverageContractRentByMonthGroup> groups)
        {
            if (groups.Any() == false)
            {
                return new List<string>();
            }

            var allDates = groups.SelectMany(x => x.Items.Select(y => new DateTime(y.Date.Year, y.Date.Month, 1))).Distinct().OrderBy(x => x).ToList();

            return allDates.Select(x => x.ToString("MMM yyyy")).ToList();
        }
    }
}
