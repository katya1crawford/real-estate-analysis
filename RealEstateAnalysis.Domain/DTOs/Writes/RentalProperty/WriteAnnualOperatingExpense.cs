namespace RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty
{
    public class WriteAnnualOperatingExpense
    {
        public WriteAnnualOperatingExpense()
        {
        }

        public WriteAnnualOperatingExpense(long operatingExpenseTypeId, decimal amount)
        {
            OperatingExpenseTypeId = operatingExpenseTypeId;
            Amount = amount;
        }

        public long OperatingExpenseTypeId { get; set; }

        public decimal Amount { get; set; }
    }
}