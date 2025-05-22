namespace RealEstateAnalysis.Domain.DTOs.Reads
{
    public class ReadGeneralRepairExpense
    {
        public ReadGeneralRepairExpense(long generalRepairExpenseTypeId, string generalRepairExpenseTypeName, decimal amount)
        {
            GeneralRepairExpenseTypeId = generalRepairExpenseTypeId;
            GeneralRepairExpenseTypeName = generalRepairExpenseTypeName;
            Amount = amount;
        }

        public long GeneralRepairExpenseTypeId { get; }

        public string GeneralRepairExpenseTypeName { get; }

        public decimal Amount { get; }
    }
}