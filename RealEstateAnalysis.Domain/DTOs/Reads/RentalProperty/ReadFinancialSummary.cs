using System;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadFinancialSummary
    {
        public ReadFinancialSummary(decimal annualCapRate,
            decimal annualEffectiveGrossIncome,
            decimal annualCashOnCashRate,
            decimal annualNetRentalIncome,
            decimal totalAnnualOperatingExpenses,
            decimal annualNoi,
            decimal annualMortgageExpenses,
            decimal annualCashFlow,
            decimal annualVacancyLoss,
            decimal annualPropertyManagementFee,
            decimal totalClosingCosts,
            decimal totalInteriorRepairExpenses,
            decimal totalExteriorRepairExpenses,
            decimal totalGeneralRepairExpenses,
            decimal loanAmount,
            decimal loanToValueRate,
            decimal totalCashNeeded,
            decimal debtCoverageRatio,
            decimal grossRentMultiplier,
            decimal twoPercentRule,
            decimal operatingExpenseRatio,
            decimal annualTotalRoi,
            decimal totalPurchasePrice,
            decimal totalImprovements,
            decimal exitPrice,
            decimal gainOnSale,
            decimal gainOnValuePercent,
            decimal annualTotalPrincipalPayment)
        {
            AnnualCapRate = Math.Round(annualCapRate, 2);
            AnnualCashFlow = Math.Round(annualCashFlow, 2);
            AnnualCashOnCashRate = Math.Round(annualCashOnCashRate, 2);
            AnnualNetRentalIncome = Math.Round(annualNetRentalIncome, 2);
            AnnualEffectiveGrossIncome = Math.Round(annualEffectiveGrossIncome, 2);
            AnnualMortgageExpenses = Math.Round(annualMortgageExpenses, 2);
            AnnualNoi = Math.Round(annualNoi, 2);
            TotalAnnualOperatingExpenses = Math.Round(totalAnnualOperatingExpenses, 2);
            AnnualVacancyLoss = Math.Round(annualVacancyLoss, 2);
            AnnualPropertyManagementFee = Math.Round(annualPropertyManagementFee, 2);
            TotalClosingCosts = Math.Round(totalClosingCosts, 2);
            TotalInteriorRepairExpenses = Math.Round(totalInteriorRepairExpenses, 2);
            TotalExteriorRepairExpenses = Math.Round(totalExteriorRepairExpenses, 2);
            TotalGeneralRepairExpenses = Math.Round(totalGeneralRepairExpenses, 2);
            LoanAmount = Math.Round(loanAmount, 2);
            LoanToValueRate = Math.Round(loanToValueRate, 2);
            TotalCashNeeded = Math.Round(totalCashNeeded, 2);
            DebtCoverageRatio = Math.Round(debtCoverageRatio, 2);
            GrossRentMultiplier = Math.Round(grossRentMultiplier, 2);
            TwoPercentRule = Math.Round(twoPercentRule, 2);
            OperatingExpenseRatio = Math.Round(operatingExpenseRatio, 2);
            AnnualTotalRoi = Math.Round(annualTotalRoi, 2);
            TotalPurchasePrice = Math.Round(totalPurchasePrice, 2);
            TotalImprovements = Math.Round(totalImprovements, 2);
            ExitPrice = Math.Round(exitPrice, 2);
            GainOnSale = Math.Round(gainOnSale, 2);
            GainOnValuePercent = Math.Round(gainOnValuePercent, 2);
            AnnualTotalPrincipalPayment = Math.Round(annualTotalPrincipalPayment, 2);
        }

        public decimal AnnualCapRate { get; }

        public decimal AnnualCashFlow { get; }

        public decimal AnnualCashOnCashRate { get; }

        public decimal AnnualEffectiveGrossIncome { get; }

        public decimal AnnualMortgageExpenses { get; }

        public decimal AnnualNetRentalIncome { get; set; }

        public decimal AnnualNoi { get; }

        public decimal AnnualPropertyManagementFee { get; }

        public decimal AnnualTotalRoi { get; }

        public decimal AnnualVacancyLoss { get; }

        public decimal DebtCoverageRatio { get; }

        public decimal ExitPrice { get; }

        public decimal GainOnSale { get; }

        public decimal GainOnValuePercent { get; }

        public decimal GrossRentMultiplier { get; }

        public decimal LoanAmount { get; }

        public decimal LoanToValueRate { get; }

        public decimal OperatingExpenseRatio { get; }

        public decimal TotalAnnualOperatingExpenses { get; }

        public decimal TotalCashNeeded { get; }

        public decimal TotalClosingCosts { get; }

        public decimal TotalExteriorRepairExpenses { get; }

        public decimal TotalGeneralRepairExpenses { get; }

        public decimal TotalImprovements { get; }

        public decimal TotalInteriorRepairExpenses { get; }

        public decimal TotalPurchasePrice { get; }

        public decimal TwoPercentRule { get; }

        public decimal AnnualTotalPrincipalPayment { get; }
    }
}