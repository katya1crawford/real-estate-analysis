namespace RealEstateAnalysis.Domain.DTOs.Writes
{
    public class WriteLoan
    {
        public WriteLoan()
        {
        }

        public WriteLoan(decimal amount, decimal apr, int years)
        {
            Amount = amount;
            Apr = apr;
            Years = years;
        }

        public decimal Amount { get; set; }

        public decimal Apr { get; set; }

        public int Years { get; set; }
    }
}