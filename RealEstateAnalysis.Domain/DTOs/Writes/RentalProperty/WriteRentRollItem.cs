using System;

namespace RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty
{
    public class WriteRentRollItem
    {
        public string Unit { get; set; }

        public int SquareFootage { get; set; }

        public int Bedrooms { get; set; }

        public double Bathrooms { get; set; }

        public bool IsVacant { get; set; }

        public bool IsRenovated { get; set; }

        public decimal? ContractRent { get; set; }

        public decimal? OtherIncome { get; set; }

        public decimal? MarketRent { get; set; }

        public DateTime? LeaseStartDate { get; set; }

        public DateTime? LeaseEndDate { get; set; }
    }
}