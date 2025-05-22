namespace RealEstateAnalysis.Domain.DTOs.Reads
{
    public class ReadInteriorRepairExpense
    {
        public ReadInteriorRepairExpense(long interiorRepairExpenseTypeId, string interiorRepairExpenseTypeName, decimal amount)
        {
            InteriorRepairExpenseTypeId = interiorRepairExpenseTypeId;
            InteriorRepairExpenseTypeName = interiorRepairExpenseTypeName;
            Amount = amount;
        }

        public long InteriorRepairExpenseTypeId { get; }

        public string InteriorRepairExpenseTypeName { get; }

        public decimal Amount { get; }
    }
}