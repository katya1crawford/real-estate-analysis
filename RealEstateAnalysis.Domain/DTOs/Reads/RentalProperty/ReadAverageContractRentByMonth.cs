using System;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadAverageContractRentByMonth
    {
        public ReadAverageContractRentByMonth(decimal averageContractRent, DateTime date)
        {
            AverageContractRent = averageContractRent;
            Category = date.ToString("MMM yyyy");
        }

        public decimal AverageContractRent { get; }

        public string Category { get; }
    }
}