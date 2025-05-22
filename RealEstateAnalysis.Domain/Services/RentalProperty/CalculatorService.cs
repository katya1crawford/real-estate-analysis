using FluentValidation;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.Abstract.RentalProperty;
using RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty;
using RealEstateAnalysis.Domain.DTOs.Writes;
using RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty;
using RealEstateAnalysis.Domain.Validators.RentalProperty;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateAnalysis.Domain.Services.RentalProperty
{
    public class CalculatorService : ICalculatorService
    {
        private readonly ILoanService loanService;

        public CalculatorService(ILoanService loanService)
        {
            this.loanService = loanService;
        }

        public ReadFinancialSummary GetFinancialSummary(WriteFinancialSummary model)
        {
            var validator = new FinancialDetailsValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var annualVacancyLoss = model.AnnualGrossScheduledRentalIncome * (model.AnnualVacancyRate / 100);
            var annualNetRentalIncome = model.AnnualGrossScheduledRentalIncome - annualVacancyLoss;
            var annualEffectiveGrossIncome = annualNetRentalIncome + model.OtherAnnualIncome;
            var annualPropertyManagementFee = annualEffectiveGrossIncome * (model.AnnualPropertyManagementFeeRate / 100);
            var totalClosingCosts = model.ClosingCosts.Sum(x => x.Amount);
            var totalInteriorRepairExpenses = model.InteriorRepairExpenses.Sum(x => x.Amount);
            var totalExteriorRepairExpenses = model.ExteriorRepairExpenses.Sum(x => x.Amount);
            var totalGeneralRepairExpenses = model.GeneralRepairExpenses.Sum(x => x.Amount);

            var totalAnnualOperatingExpenses = model.AnnualOperatingExpenses.Sum(x => x.Amount) + annualPropertyManagementFee;
            var annualNoi = annualEffectiveGrossIncome - totalAnnualOperatingExpenses;
            var loanModel = new WriteLoan()
            {
                Amount = model.PurchasePrice - model.DownPayment,
                Apr = model.LoanApr,
                Years = model.LoanYears
            };
            var totalImprovements = totalInteriorRepairExpenses + totalExteriorRepairExpenses + totalGeneralRepairExpenses;
            var totalPurchasePrice = model.PurchasePrice + totalClosingCosts + totalImprovements;
            var annualMortgageExpenses = (decimal)(loanService.GetMonthlyLoanPaymentAmount(loanModel) * 12);
            var loanToValueRate = (loanModel.Amount / model.PurchasePrice) * 100;
            var annualCashFlow = annualNoi - annualMortgageExpenses;
            var capRate = (annualNoi / totalPurchasePrice) * 100;
            var totalCashNeeded = model.DownPayment + totalClosingCosts + totalImprovements;
            var annualCashOnCashRate = GetCashOnCashRate(totalCashNeeded, annualCashFlow);
            var debtCoverageRatio = annualMortgageExpenses > 0 ? (annualNoi / annualMortgageExpenses) : 0;
            var grossRentMultiplier = totalPurchasePrice / model.AnnualGrossScheduledRentalIncome;
            var twoPercentRule = ((model.AnnualGrossScheduledRentalIncome / 12) / totalPurchasePrice) * 100;
            var operatingExpenseRatio = (totalAnnualOperatingExpenses / annualEffectiveGrossIncome) * 100;
            var annualTotalPrincipalPayment = (decimal)loanService.GetTotalPrincipalPaid(years: 1, model: loanModel);
            var annualTotalRoi = GetTotalRoi(annualCashFlow, totalCashNeeded, annualTotalPrincipalPayment);
            var exitPrice = model.MarketCapitalizationRate == 0 ? 0 : annualNoi / (model.MarketCapitalizationRate / 100);
            var gainOnSale = exitPrice == 0 ? 0 : exitPrice - totalPurchasePrice;
            var gainOnValuePercent = exitPrice == 0 ? 0 : (gainOnSale / exitPrice) * 100;

            return new ReadFinancialSummary(annualCapRate: capRate,
                annualCashOnCashRate: annualCashOnCashRate,
                annualNetRentalIncome: annualNetRentalIncome,
                annualEffectiveGrossIncome: annualEffectiveGrossIncome,
                totalAnnualOperatingExpenses: totalAnnualOperatingExpenses,
                annualNoi: annualNoi,
                annualMortgageExpenses: annualMortgageExpenses,
                annualCashFlow: annualCashFlow,
                annualVacancyLoss: annualVacancyLoss,
                annualPropertyManagementFee: annualPropertyManagementFee,
                totalClosingCosts: totalClosingCosts,
                totalInteriorRepairExpenses: totalInteriorRepairExpenses,
                totalExteriorRepairExpenses: totalExteriorRepairExpenses,
                totalGeneralRepairExpenses: totalGeneralRepairExpenses,
                loanAmount: loanModel.Amount,
                loanToValueRate: loanToValueRate,
                totalCashNeeded: totalCashNeeded,
                debtCoverageRatio: debtCoverageRatio,
                grossRentMultiplier: grossRentMultiplier,
                twoPercentRule: twoPercentRule,
                operatingExpenseRatio: operatingExpenseRatio,
                annualTotalRoi: annualTotalRoi,
                totalPurchasePrice: totalPurchasePrice,
                totalImprovements: totalImprovements,
                exitPrice: exitPrice,
                gainOnSale: gainOnSale,
                gainOnValuePercent: gainOnValuePercent,
                annualTotalPrincipalPayment: annualTotalPrincipalPayment);
        }

        public List<ReadFinancialForecast> GetLongTermFinancialForecasts(ReadProperty property)
        {
            var forecastYears = 30;
            var financialForecast = new List<ReadFinancialForecast>();
            var annualGrossScheduledRentalIncome = GetFinancialForecastValues(property.AnnualGrossScheduledRentalIncome, property.AnnualGrossScheduledRentalIncomeGrowthRate, forecastYears);

            var grossScheduledRentalIncome = new ReadFinancialForecast(id: 1,
                name: "Gross Scheduled Rental Income",
                values: annualGrossScheduledRentalIncome);
            financialForecast.Add(grossScheduledRentalIncome);

            var vacancyLoss = new ReadFinancialForecast(id: 2,
                name: "Less: Vacancy Loss",
                values: GetVacancyLossFinancialForecastValues(annualGrossScheduledRentalIncome, property.AnnualVacancyRate));
            financialForecast.Add(vacancyLoss);

            var netRentalIncome = new ReadFinancialForecast(id: 3,
                name: "Net Rental Income",
                values: GetFinancialForecastSubtractValues(grossScheduledRentalIncome.Values, vacancyLoss.Values));
            financialForecast.Add(netRentalIncome);

            var otherIncome = new ReadFinancialForecast(id: 4,
                name: "Other Income",
                values: GetFinancialForecastValues(property.OtherAnnualIncome, property.AnnualGrossScheduledRentalIncomeGrowthRate, forecastYears));
            financialForecast.Add(otherIncome);

            var egi = new ReadFinancialForecast(id: 5,
                name: "Effective Gross Income",
                values: GetFinancialForecastAddValues(netRentalIncome.Values, otherIncome.Values));
            financialForecast.Add(egi);

            var operatingExpenses = new ReadFinancialForecast(id: 6,
                name: "Operating Expenses",
                values: GetFinancialForecastValues(property.FinancialSummary.TotalAnnualOperatingExpenses, property.AnnualOperatingExpensesGrowthRate, forecastYears));
            financialForecast.Add(operatingExpenses);

            var noi = new ReadFinancialForecast(id: 7,
                name: "Net Operating Income",
                values: GetFinancialForecastSubtractValues(egi.Values, operatingExpenses.Values));
            financialForecast.Add(noi);

            var debtService = new ReadFinancialForecast(id: 8,
                name: "Debt Service",
                values: GetDebtServiceFinancialForecastValues(property.FinancialSummary.AnnualMortgageExpenses, property.LoanYears, forecastYears));
            financialForecast.Add(debtService);

            var netCashFlow = new ReadFinancialForecast(id: 9,
                name: "Net Cash Flow",
                values: GetFinancialForecastSubtractValues(noi.Values, debtService.Values));
            financialForecast.Add(netCashFlow);

            var cashOnCashRate = new ReadFinancialForecast(id: 10,
                name: "Cash-on-Cash Rate",
                values: GetCashOnCashRateFinancialForecastValues(netCashFlow.Values, property.FinancialSummary.TotalCashNeeded));
            financialForecast.Add(cashOnCashRate);

            return financialForecast;
        }

        private List<decimal> GetFinancialForecastValues(decimal amount, decimal rate, int periods)
        {
            var values = new List<decimal> { amount };
            var growingAmount = amount;

            for (var i = 0; i < periods - 1; i++)
            {
                var growth = growingAmount * (rate / 100);
                growingAmount += growth;

                values.Add(growingAmount);
            }

            return values;
        }

        private List<decimal> GetDebtServiceFinancialForecastValues(decimal interestAndPrincipal, int termYears, int periods)
        {
            var values = new List<decimal> { interestAndPrincipal };

            for (var i = 0; i < periods - 1; i++)
            {
                if (i + 1 >= termYears)
                    values.Add(0);
                else
                    values.Add(interestAndPrincipal);
            }

            return values;
        }

        private List<decimal> GetFinancialForecastSubtractValues(params List<decimal>[] forcastValues)
        {
            var sumValues = new decimal[forcastValues[0].Count()];

            for (var i = 0; i < sumValues.Count(); i++)
            {
                decimal sum = 0;

                for (var z = 0; z < forcastValues.Count(); z++)
                {
                    if (z == 0)
                    {
                        sum = forcastValues[z][i];
                        continue;
                    }

                    sum -= forcastValues[z][i];
                }

                sumValues[i] = sum;
            }

            return sumValues.ToList();
        }

        private List<decimal> GetFinancialForecastAddValues(params List<decimal>[] forcastValues)
        {
            var sumValues = new decimal[forcastValues[0].Count()];

            for (var i = 0; i < sumValues.Count(); i++)
            {
                decimal sum = 0;

                for (var z = 0; z < forcastValues.Count(); z++)
                {
                    if (z == 0)
                    {
                        sum = forcastValues[z][i];
                        continue;
                    }

                    sum += forcastValues[z][i];
                }

                sumValues[i] = sum;
            }

            return sumValues.ToList();
        }

        private List<decimal> GetVacancyLossFinancialForecastValues(List<decimal> annualGrossScheduledRentalIncomeForcast, decimal vacancyRate)
        {
            var vacancyValues = new decimal[annualGrossScheduledRentalIncomeForcast.Count()];

            for (var i = 0; i < vacancyValues.Count(); i++)
            {
                vacancyValues[i] = annualGrossScheduledRentalIncomeForcast[i] * (vacancyRate / 100); ;
            }

            return vacancyValues.ToList();
        }

        private List<decimal> GetCashOnCashRateFinancialForecastValues(List<decimal> netCashFlowForcast, decimal totalCashInvested)
        {
            var cashOnCashRateValues = new decimal[netCashFlowForcast.Count()];

            for (var i = 0; i < cashOnCashRateValues.Count(); i++)
            {
                cashOnCashRateValues[i] = GetCashOnCashRate(totalCashInvested, netCashFlowForcast[i]);
            }

            return cashOnCashRateValues.ToList();
        }

        private decimal GetCashOnCashRate(decimal totalCashInvested, decimal cashFlow) =>
            totalCashInvested > 0 ? (cashFlow / totalCashInvested) * 100 : 0;

        private decimal GetTotalRoi(decimal cashFlow, decimal totalCashInvested, decimal totalPrincipalPayment) =>
            totalCashInvested > 0 ? ((cashFlow + totalPrincipalPayment) / totalCashInvested) * 100 : 0;
    }
}