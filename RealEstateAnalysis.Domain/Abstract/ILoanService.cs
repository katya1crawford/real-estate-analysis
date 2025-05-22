using RealEstateAnalysis.Domain.DTOs.Reads;
using RealEstateAnalysis.Domain.DTOs.Writes;
using System.Collections.Generic;

namespace RealEstateAnalysis.Domain.Abstract
{
    public interface ILoanService
    {
        List<ReadMonthlyLoanAmortizationDetail> CalculateMonthlyLoanAmortizationDetails(WriteLoan model);

        double GetMonthlyLoanPaymentAmount(WriteLoan model);

        double GetTotalPrincipalPaid(int years, WriteLoan model);
    }
}