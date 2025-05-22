using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.DTOs.Writes;
using RealEstateAnalysis.Domain.Services;
using System.Linq;
using Xunit;

namespace RealEstateAnalysis.Tests.Domain
{
    public class LoanServiceTests
    {
        private readonly ILoanService loanService;

        public LoanServiceTests()
        {
            loanService = new LoanService();
        }

        [Fact]
        public void MonthlyLoanPaymentAmountIsValid()
        {
            //Arrange
            var writeLoan = new WriteLoan
            {
                Amount = 300000,
                Apr = 6,
                Years = 30
            };

            //Act
            var result = loanService.GetMonthlyLoanPaymentAmount(writeLoan);

            //Assert
            Assert.Equal(1798.6515754582706, result);
        }

        [Fact]
        public void LoanAmortizationScheduleIsValid()
        {
            //Arrange
            var writeLoan = new WriteLoan
            {
                Amount = 300000,
                Apr = 6,
                Years = 30
            };

            //Act
            var result = loanService.CalculateMonthlyLoanAmortizationDetails(writeLoan);
            var totalInterestPaid = result.Sum(x => x.InterestPaid);
            var totalPrincipalPaid = result.Sum(x => x.PrincipalPaid);

            //Assert
            Assert.Equal(347514.56000000023, totalInterestPaid);
            Assert.Equal(299999.8800000002, totalPrincipalPaid);
        }

        [Fact]
        public void TotalPrincipalPaidIsValid()
        {
            //Arrange
            var writeLoan = new WriteLoan
            {
                Amount = 300000,
                Apr = 6,
                Years = 30
            };

            //Act
            var result = loanService.GetTotalPrincipalPaid(years: 1, model: writeLoan);

            //Assert
            Assert.Equal(3684.01, result);
        }
    }
}