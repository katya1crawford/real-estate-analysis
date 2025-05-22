namespace RealEstateAnalysis.Domain.DTOs.Writes
{
    public class WriteClosingCost
    {
        public WriteClosingCost()
        {
        }

        public WriteClosingCost(long closingCostTypeId, decimal amount)
        {
            ClosingCostTypeId = closingCostTypeId;
            Amount = amount;
        }

        public long ClosingCostTypeId { get; set; }

        public decimal Amount { get; set; }
    }
}