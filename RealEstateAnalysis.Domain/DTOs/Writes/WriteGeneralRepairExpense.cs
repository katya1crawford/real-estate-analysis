namespace RealEstateAnalysis.Domain.DTOs.Writes
{
    public class WriteGeneralRepairExpense
    {
        public WriteGeneralRepairExpense()
        {
        }

        public WriteGeneralRepairExpense(long generalRepairExpenseTypeId, decimal amount)
        {
            GeneralRepairExpenseTypeId = generalRepairExpenseTypeId;
            Amount = amount;
        }

        public long GeneralRepairExpenseTypeId { get; set; }

        public decimal Amount { get; set; }
    }
}