using System;

namespace RealEstateAnalysis.Domain.DTOs.Reads
{
    public class ReadMonthlyLoanAmortizationDetail
    {
        public ReadMonthlyLoanAmortizationDetail(int month, double payment, double interestPaid, double principalPaid, double balance)
        {
            Month = month;
            Payment = Math.Round(payment, 2);
            InterestPaid = Math.Round(interestPaid, 2);
            PrincipalPaid = Math.Round(principalPaid, 2);
            Balance = Math.Round(balance, 2);
        }

        public int Month { get; }

        public double Payment { get; }

        public double InterestPaid { get; }

        public double PrincipalPaid { get; }

        public double Balance { get; }
    }
}