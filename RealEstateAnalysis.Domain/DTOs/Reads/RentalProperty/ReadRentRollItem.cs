using RealEstateAnalysis.Data.Entities.RentalProperty;
using System;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadRentRollItem
    {
        public ReadRentRollItem(RentRollItem rentRollItem)
        {
            Id = rentRollItem.Id;
            Unit = rentRollItem.Unit;
            SquareFootage = rentRollItem.SquareFootage;
            Bedrooms = rentRollItem.Bedrooms;
            Bathrooms = rentRollItem.Bathrooms;
            IsVacant = rentRollItem.IsVacant;
            IsRenovated = rentRollItem.IsRenovated;
            ContractRent = rentRollItem.ContractRent;
            OtherIncome = rentRollItem.OtherIncome;
            MarketRent = rentRollItem.MarketRent;
            LeaseStartDate = rentRollItem.LeaseStartDate;
            LeaseEndDate = rentRollItem.LeaseEndDate;
            LeaseTermInMonths = GetLeaseTermInMonths(rentRollItem);
            ContractRentPerSquareFoot = Math.Round(((rentRollItem.ContractRent ?? 0) / rentRollItem.SquareFootage), 2);
            MarketRentPerSquareFoot = Math.Round(((rentRollItem.MarketRent ?? 0) / rentRollItem.SquareFootage), 2);
            IsMonthToMonth = rentRollItem.IsVacant == false && rentRollItem.LeaseStartDate == null;
            FloorPlan = GetFloorPlan(Bedrooms, Bathrooms, SquareFootage);
            PercentOfMarketRent = Math.Round(Divide(ContractRent ?? 0, MarketRent ?? 0) * 100, 2);
        }

        public long Id { get; }

        public string Unit { get; }

        public string FloorPlan { get; }

        public int SquareFootage { get; }

        public int Bedrooms { get; }

        public double Bathrooms { get; }

        public bool IsVacant { get; }

        public bool IsRenovated { get; }

        public decimal? ContractRent { get; }

        public decimal? OtherIncome { get; }

        public decimal? MarketRent { get; }

        public DateTime? LeaseStartDate { get; }

        public DateTime? LeaseEndDate { get; }

        public int? LeaseTermInMonths { get; }

        public decimal? ContractRentPerSquareFoot { get; }

        public decimal? MarketRentPerSquareFoot { get; }

        public bool IsMonthToMonth { get; }

        public decimal PercentOfMarketRent { get; }

        private int? GetLeaseTermInMonths(RentRollItem rentRollItem)
        {
            if (rentRollItem.LeaseStartDate == null && rentRollItem.LeaseEndDate == null)
            {
                return null;
            }

            return ((rentRollItem.LeaseEndDate.Value.Year - rentRollItem.LeaseStartDate.Value.Year) * 12) + (rentRollItem.LeaseEndDate.Value.Month - rentRollItem.LeaseStartDate.Value.Month);
        }

        private string GetFloorPlan(int bedrooms, double bathrooms, int squareFootage) =>
            $"{bedrooms}x{bathrooms}x{squareFootage}";

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