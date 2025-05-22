using RealEstateAnalysis.Domain.Abstract.RentalProperty;
using RealEstateAnalysis.Domain.DTOs.Writes;
using RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty;
using RealEstateAnalysis.Domain.Enums;
using RealEstateAnalysis.Domain.Services;
using RealEstateAnalysis.Domain.Services.RentalProperty;
using System.Collections.Generic;
using Xunit;

namespace RealEstateAnalysis.Tests.Domain
{
    public class CalculatorServiceTests
    {
        private readonly ICalculatorService calculatorService;

        public CalculatorServiceTests()
        {
            calculatorService = new CalculatorService(new LoanService());
        }

        [Fact]
        public void FinancialSummaryIsValid()
        {
            //Arrange
            var writeFinancialSummary = new WriteFinancialSummary
            {
                AnnualOperatingExpenses = new List<WriteAnnualOperatingExpense>
                {
                    new WriteAnnualOperatingExpense{ Amount = 20000, OperatingExpenseTypeId = (long)OperatingExpenseTypeEnum.Taxes },
                    new WriteAnnualOperatingExpense{ Amount = 2000, OperatingExpenseTypeId = (long)OperatingExpenseTypeEnum.HOA },
                    new WriteAnnualOperatingExpense{ Amount = 15000, OperatingExpenseTypeId = (long)OperatingExpenseTypeEnum.LegalAndProfessionalFees },
                    new WriteAnnualOperatingExpense{ Amount = 5600, OperatingExpenseTypeId = (long)OperatingExpenseTypeEnum.CleaningAndMaintenance }
                },
                AnnualOperatingExpensesGrowthRate = 0,
                AnnualGrossScheduledRentalIncomeGrowthRate = 0,
                AnnualGrossScheduledRentalIncome = 175000,
                AnnualVacancyRate = 5,
                AnnualPropertyManagementFeeRate = 4,
                ClosingCosts = new List<WriteClosingCost>
                {
                    new WriteClosingCost{ Amount = 2500, ClosingCostTypeId = (long)ClosingCostTypeEnum.AttorneyCharges },
                    new WriteClosingCost{ Amount = 750, ClosingCostTypeId = (long)ClosingCostTypeEnum.InspectionCosts },
                    new WriteClosingCost{ Amount = 12000, ClosingCostTypeId = (long)ClosingCostTypeEnum.TitleAndEscrowFees }
                },
                DownPayment = 250000,
                ExteriorRepairExpenses = new List<WriteExteriorRepairExpense>
                {
                    new WriteExteriorRepairExpense{ Amount = 1000, ExteriorRepairExpenseTypeId = (long)ExteriorRepairExpenseTypeEnum.Concrete },
                    new WriteExteriorRepairExpense{ Amount = 450, ExteriorRepairExpenseTypeId = (long)ExteriorRepairExpenseTypeEnum.Garage },
                    new WriteExteriorRepairExpense{ Amount = 900, ExteriorRepairExpenseTypeId = (long)ExteriorRepairExpenseTypeEnum.Gutters }
                },
                GeneralRepairExpenses = new List<WriteGeneralRepairExpense>
                {
                    new WriteGeneralRepairExpense{ Amount = 350, GeneralRepairExpenseTypeId = (long)GeneralRepairExpenseTypeEnum.Mold },
                    new WriteGeneralRepairExpense{ Amount = 150, GeneralRepairExpenseTypeId = (long)GeneralRepairExpenseTypeEnum.Permits }
                },
                InteriorRepairExpenses = new List<WriteInteriorRepairExpense>
                {
                    new WriteInteriorRepairExpense{ Amount = 350, InteriorRepairExpenseTypeId = (long)InteriorRepairExpenseTypeEnum.Cabinets },
                    new WriteInteriorRepairExpense{ Amount = 1500, InteriorRepairExpenseTypeId = (long)InteriorRepairExpenseTypeEnum.Carpentry },
                    new WriteInteriorRepairExpense{ Amount = 2500, InteriorRepairExpenseTypeId = (long)InteriorRepairExpenseTypeEnum.HVAC }
                },
                LoanApr = 3.75M,
                LoanYears = 25,
                OtherAnnualIncome = 7000,
                PurchasePrice = 1000000,
                MarketCapitalizationRate = 8
            };

            //Act
            var result = calculatorService.GetFinancialSummary(writeFinancialSummary);

            //Assert
            Assert.Equal(12.10M, result.AnnualCapRate);
            Assert.Equal(28.43M, result.AnnualCashOnCashRate);
            Assert.Equal(173250, result.AnnualEffectiveGrossIncome);
            Assert.Equal(49530, result.TotalAnnualOperatingExpenses);
            Assert.Equal(123720, result.AnnualNoi);
            Assert.Equal(46271.81M, result.AnnualMortgageExpenses);
            Assert.Equal(77448.19M, result.AnnualCashFlow);
            Assert.Equal(272450, result.TotalCashNeeded);
            Assert.Equal(166250, result.AnnualNetRentalIncome);
            Assert.Equal(8750, result.AnnualVacancyLoss);
            Assert.Equal(6930, result.AnnualPropertyManagementFee);
            Assert.Equal(2.67M, result.DebtCoverageRatio);
            Assert.Equal(5.84M, result.GrossRentMultiplier);
            Assert.Equal(75, result.LoanToValueRate);
            Assert.Equal(28.59M, result.OperatingExpenseRatio);
            Assert.Equal(15250, result.TotalClosingCosts);
            Assert.Equal(2350, result.TotalExteriorRepairExpenses);
            Assert.Equal(500, result.TotalGeneralRepairExpenses);
            Assert.Equal(4350, result.TotalInteriorRepairExpenses);
            Assert.Equal(1.43M, result.TwoPercentRule);
            Assert.Equal(35.20M, result.AnnualTotalRoi);
            Assert.Equal(1022450, result.TotalPurchasePrice);
            Assert.Equal(7200, result.TotalImprovements);
            Assert.Equal(1546500, result.ExitPrice);
            Assert.Equal(524050, result.GainOnSale);
            Assert.Equal(33.89M, result.GainOnValuePercent);
        }
    }
}