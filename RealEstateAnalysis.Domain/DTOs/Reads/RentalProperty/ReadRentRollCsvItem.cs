namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadRentRollCsvItem
    {
        public string Unit { get; set; }

        public string SquareFootage { get; set; }

        public string Bedrooms { get; set; }

        public string Bathrooms { get; set; }

        public string IsVacant { get; set; }

        public string IsRenovated { get; set; }

        public string ContractRent { get; set; }

        public string OtherIncome { get; set; }

        public string MarketRent { get; set; }

        public string LeaseStartDate { get; set; }

        public string LeaseEndDate { get; set; }
    }
}