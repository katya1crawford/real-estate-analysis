using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty
{
    public class WriteProperty
    {
        public WriteAddress Address { get; set; }

        public decimal AnnualGrossScheduledRentalIncome { get; set; }

        public decimal AnnualGrossScheduledRentalIncomeGrowthRate { get; set; }

        public List<WriteAnnualOperatingExpense> AnnualOperatingExpenses { get; set; }

        public decimal AnnualOperatingExpensesGrowthRate { get; set; }

        public decimal AnnualPropertyManagementFeeRate { get; set; }

        public decimal AnnualVacancyRate { get; set; }

        public int BuildingSquareFootage { get; set; }

        public List<WriteClosingCost> ClosingCosts { get; set; }

        public decimal DownPayment { get; set; }

        public List<WriteExteriorRepairExpense> ExteriorRepairExpenses { get; set; }

        public List<WriteGeneralRepairExpense> GeneralRepairExpenses { get; set; }

        public string GroupName { get; set; }

        public List<WriteInteriorRepairExpense> InteriorRepairExpenses { get; set; }

        public decimal LoanApr { get; set; }

        public int LoanYears { get; set; }

        public int LotSquareFootage { get; set; }

        public decimal MarketCapitalizationRate { get; set; }

        public string Notes { get; set; }

        public decimal OtherAnnualIncome { get; set; }

        public long PropertyTypeId { get; set; }

        public long PropertyStatusId { get; set; }

        public decimal PurchasePrice { get; set; }

        public IFormFile ThumbnailImage { get; set; }

        public List<WriteUnitGroup> UnitGroups { get; set; }

        public int YearBuiltIn { get; set; }
    }
}