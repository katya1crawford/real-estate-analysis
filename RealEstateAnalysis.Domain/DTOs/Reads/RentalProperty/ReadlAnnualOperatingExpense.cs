using RealEstateAnalysis.Data.Entities.RentalProperty;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadlAnnualOperatingExpense
    {
        public ReadlAnnualOperatingExpense(AnnualOperatingExpense annualOperatingExpense)
        {
            Id = annualOperatingExpense.Id;
            OperatingExpenseTypeId = annualOperatingExpense.OperatingExpenseType.Id;
            Amount = annualOperatingExpense.Amount;
            OperatingExpenseTypeName = annualOperatingExpense.OperatingExpenseType.Name;
        }

        public long Id { get; }

        public long OperatingExpenseTypeId { get; }

        public string OperatingExpenseTypeName { get; }

        public decimal Amount { get; }
    }
}