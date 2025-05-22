using System;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadLeasesCountByMonth
    {
        public ReadLeasesCountByMonth(int count, DateTime date)
        {
            Count = count;
            Category = date.ToString("MMM yyyy");
        }

        public int Count { get; }

        public string Category { get; }
    }
}