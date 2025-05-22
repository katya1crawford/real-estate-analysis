using System;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadFloorPlanAverageContractRentByMonth
    {
        public ReadFloorPlanAverageContractRentByMonth(decimal averageContractRent, DateTime date)
        {
            AverageContractRent = averageContractRent;
            Category = date.ToString("MMM yyyy");
            Date = date;
        }

        public decimal AverageContractRent { get; }

        public string Category { get; }

        public DateTime Date { get; }
    }
}