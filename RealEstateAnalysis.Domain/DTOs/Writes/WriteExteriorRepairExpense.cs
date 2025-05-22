namespace RealEstateAnalysis.Domain.DTOs.Writes
{
    public class WriteExteriorRepairExpense
    {
        public WriteExteriorRepairExpense()
        {
        }

        public WriteExteriorRepairExpense(long exteriorRepairExpenseTypeId, decimal amount)
        {
            ExteriorRepairExpenseTypeId = exteriorRepairExpenseTypeId;
            Amount = amount;
        }

        public long ExteriorRepairExpenseTypeId { get; set; }

        public decimal Amount { get; set; }
    }
}