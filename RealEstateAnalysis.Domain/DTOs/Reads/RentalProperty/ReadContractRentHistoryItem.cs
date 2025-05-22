using System;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadContractRentHistoryItem
    {
        public decimal ContractRent { get; }

        public DateTime Date { get; }
    }
}