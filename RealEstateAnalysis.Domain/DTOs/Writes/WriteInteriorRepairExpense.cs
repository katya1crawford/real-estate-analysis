namespace RealEstateAnalysis.Domain.DTOs.Writes
{
    public class WriteInteriorRepairExpense
    {
        public WriteInteriorRepairExpense()
        {
        }

        public WriteInteriorRepairExpense(long interiorRepairExpenseTypeId, decimal amount)
        {
            InteriorRepairExpenseTypeId = interiorRepairExpenseTypeId;
            Amount = amount;
        }

        public long InteriorRepairExpenseTypeId { get; set; }

        public decimal Amount { get; set; }
    }
}