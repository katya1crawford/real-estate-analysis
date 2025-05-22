namespace RealEstateAnalysis.Domain.DTOs.Reads
{
    public class ReadExteriorRepairExpense
    {
        public ReadExteriorRepairExpense(long exteriorRepairExpenseTypeId, string exteriorRepairExpenseTypeName, decimal amount)
        {
            ExteriorRepairExpenseTypeId = exteriorRepairExpenseTypeId;
            ExteriorRepairExpenseTypeName = exteriorRepairExpenseTypeName;
            Amount = amount;
        }

        public long ExteriorRepairExpenseTypeId { get; }

        public string ExteriorRepairExpenseTypeName { get; }

        public decimal Amount { get; }
    }
}