using System.Collections.Generic;

namespace RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty
{
    public class WriteFinancialSummary
    {
        public WriteFinancialSummary()
        {
        }

        public WriteFinancialSummary(decimal purchasePrice,
            decimal downPayment,
            decimal annualGrossScheduledRentalIncome,
            decimal otherAnnualIncome,
            decimal annualVacancyRate,
            decimal annualPropertyManagementFeeRate,
            decimal loanApr,
            int loanYears,
            decimal annualGrossScheduledRentalIncomeGrowthRate,
            decimal annualOperatingExpensesGrowthRate,
            decimal marketCapitalizationRate,
            List<WriteClosingCost> closingCosts,
            List<WriteAnnualOperatingExpense> annualOperatingExpenses,
            List<WriteExteriorRepairExpense> exteriorRepairExpenses,
            List<WriteGeneralRepairExpense> generalRepairExpenses,
            List<WriteInteriorRepairExpense> interiorRepairExpenses)
        {
            PurchasePrice = purchasePrice;
            DownPayment = downPayment;
            AnnualGrossScheduledRentalIncome = annualGrossScheduledRentalIncome;
            OtherAnnualIncome = otherAnnualIncome;
            AnnualVacancyRate = annualVacancyRate;
            AnnualPropertyManagementFeeRate = annualPropertyManagementFeeRate;
            LoanApr = loanApr;
            LoanYears = loanYears;
            AnnualGrossScheduledRentalIncomeGrowthRate = annualGrossScheduledRentalIncomeGrowthRate;
            AnnualOperatingExpensesGrowthRate = annualOperatingExpensesGrowthRate;
            ClosingCosts = closingCosts;
            AnnualOperatingExpenses = annualOperatingExpenses;
            ExteriorRepairExpenses = exteriorRepairExpenses;
            GeneralRepairExpenses = generalRepairExpenses;
            InteriorRepairExpenses = interiorRepairExpenses;
            MarketCapitalizationRate = marketCapitalizationRate;
        }

        public List<WriteAnnualOperatingExpense> AnnualOperatingExpenses { get; set; }

        public decimal AnnualOperatingExpensesGrowthRate { get; set; }

        public decimal AnnualGrossScheduledRentalIncomeGrowthRate { get; set; }

        public decimal AnnualGrossScheduledRentalIncome { get; set; }

        public decimal AnnualVacancyRate { get; set; }

        public decimal AnnualPropertyManagementFeeRate { get; set; }

        public List<WriteClosingCost> ClosingCosts { get; set; }

        public decimal DownPayment { get; set; }

        public List<WriteExteriorRepairExpense> ExteriorRepairExpenses { get; set; }

        public List<WriteGeneralRepairExpense> GeneralRepairExpenses { get; set; }

        public List<WriteInteriorRepairExpense> InteriorRepairExpenses { get; set; }

        public decimal LoanApr { get; set; }

        public int LoanYears { get; set; }

        public decimal OtherAnnualIncome { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal MarketCapitalizationRate { get; set; }
    }
}