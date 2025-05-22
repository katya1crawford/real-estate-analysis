namespace RealEstateAnalysis.Domain.DTOs.Reads
{
    public class ReadClosingCost
    {
        public ReadClosingCost(long closingCostTypeId, string closingCostTypeName, decimal amount)
        {
            ClosingCostTypeId = closingCostTypeId;
            ClosingCostTypeName = closingCostTypeName;
            Amount = amount;
        }

        public long ClosingCostTypeId { get; }

        public string ClosingCostTypeName { get; }

        public decimal Amount { get; }
    }
}