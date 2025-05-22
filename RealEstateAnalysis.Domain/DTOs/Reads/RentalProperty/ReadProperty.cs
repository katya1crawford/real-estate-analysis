using RealEstateAnalysis.Data.Entities.RentalProperty;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty
{
    public class ReadProperty
    {
        public ReadProperty(Property property,
            ReadFinancialSummary financialDetails,
            List<ReadNearbyPlace> nearbyGroceryOrSupermarkets,
            List<ReadNearbyPlace> nearbyStarbuckses,
            List<ReadNearbyPlace> nearbyPawnShops,
            List<ReadNearbyPlace> nearbyCheckCashingPlaces)
        {
            Id = property.Id;
            Address = new ReadAddress(property);
            CreatedDate = property.CreatedDate;
            Notes = property.Notes;
            FinancialSummary = financialDetails;
            PropertyType = new ReadLookup(id: property.PropertyType.Id, name: property.PropertyType.Name);
            PropertyStatus = new ReadLookup(id: property.PropertyStatus.Id, name: property.PropertyStatus.Name);
            BuildingSquareFootage = property.BuildingSquareFootage;
            ThumbnailImageBase64 = property.ThumbnailImage != null ? Convert.ToBase64String(property.ThumbnailImage) : string.Empty;
            ThumbnailImageContentType = property.ThumbnailImageContentType;
            LotSquareFootage = property.LotSquareFootage;
            YearBuiltIn = property.YearBuiltIn;
            AnnualOperatingExpensesGrowthRate = property.AnnualOperatingExpensesGrowthRate;
            AnnualGrossScheduledRentalIncomeGrowthRate = property.AnnualGrossScheduledRentalIncomeGrowthRate;
            AnnualGrossScheduledRentalIncome = property.AnnualGrossScheduledRentalIncome;
            AnnualVacancyRate = property.AnnualVacancyRate;
            AnnualPropertyManagementFeeRate = property.AnnualPropertyManagementFeeRate;
            DownPayment = property.DownPayment;
            LoanApr = property.LoanApr;
            LoanYears = property.LoanYears;
            OtherAnnualIncome = property.OtherAnnualIncome;
            PurchasePrice = property.PurchasePrice;
            TotalUnits = property.UnitGroups.Sum(x => x.NumberOfUnits);
            TotalUnitsSquareFootage = property.UnitGroups.Sum(x => x.SquareFootage * x.NumberOfUnits);
            AnnualOperatingExpenses = property.AnnualOperatingExpenses.Select(x => new ReadlAnnualOperatingExpense(x)).ToList();
            ClosingCosts = property.ClosingCosts.Select(x => new ReadClosingCost(closingCostTypeId: x.ClosingCostType.Id, closingCostTypeName: x.ClosingCostType.Name, amount: x.Amount)).ToList();
            ExteriorRepairExpenses = property.ExteriorRepairExpenses.Select(x => new ReadExteriorRepairExpense(exteriorRepairExpenseTypeId: x.ExteriorRepairExpenseType.Id, exteriorRepairExpenseTypeName: x.ExteriorRepairExpenseType.Name, amount: x.Amount)).ToList();
            InteriorRepairExpenses = property.InteriorRepairExpenses.Select(x => new ReadInteriorRepairExpense(interiorRepairExpenseTypeId: x.InteriorRepairExpenseType.Id, interiorRepairExpenseTypeName: x.InteriorRepairExpenseType.Name, amount: x.Amount)).ToList();
            GeneralRepairExpenses = property.GeneralRepairExpenses.Select(x => new ReadGeneralRepairExpense(generalRepairExpenseTypeId: x.GeneralRepairExpenseType.Id, generalRepairExpenseTypeName: x.GeneralRepairExpenseType.Name, amount: x.Amount)).ToList();
            Files = property.Files.Select(x => new ReadFile(id: x.Id, name: x.Name, bytes: default, mimeType: x.ContentType, createdDate: x.CreatedDate)).ToList();
            UnitGroups = property.UnitGroups.Select(x => new ReadUnitGroup(id: x.Id, bathrooms: x.Bathrooms, bedrooms: x.Bedrooms, numberOfUnits: x.NumberOfUnits, squareFootage: x.SquareFootage)).ToList();
            NearbyGroceryOrSupermarkets = nearbyGroceryOrSupermarkets;
            NearbyStarbuckses = nearbyStarbuckses;
            NearbyPawnShops = nearbyPawnShops;
            NearbyCheckCashingPlaces = nearbyCheckCashingPlaces;
            MarketCapitalizationRate = property.MarketCapitalizationRate;
            GroupName = property.GroupName;
        }

        public ReadAddress Address { get; }

        public decimal AnnualGrossScheduledRentalIncome { get; }

        public decimal AnnualGrossScheduledRentalIncomeGrowthRate { get; }

        public List<ReadlAnnualOperatingExpense> AnnualOperatingExpenses { get; }

        public decimal AnnualOperatingExpensesGrowthRate { get; }

        public decimal AnnualPropertyManagementFeeRate { get; }

        public decimal AnnualVacancyRate { get; }

        public int BuildingSquareFootage { get; }

        public List<ReadClosingCost> ClosingCosts { get; }

        public DateTime CreatedDate { get; }

        public decimal DownPayment { get; }

        public List<ReadExteriorRepairExpense> ExteriorRepairExpenses { get; }

        public List<ReadFile> Files { get; }

        public ReadFinancialSummary FinancialSummary { get; }

        public List<ReadGeneralRepairExpense> GeneralRepairExpenses { get; }

        public string GroupName { get; }

        public long Id { get; }

        public List<ReadInteriorRepairExpense> InteriorRepairExpenses { get; }

        public decimal LoanApr { get; }

        public int LoanYears { get; }

        public int LotSquareFootage { get; }

        public decimal MarketCapitalizationRate { get; }

        public List<ReadNearbyPlace> NearbyCheckCashingPlaces { get; }

        public List<ReadNearbyPlace> NearbyGroceryOrSupermarkets { get; }

        public List<ReadNearbyPlace> NearbyPawnShops { get; }

        public List<ReadNearbyPlace> NearbyStarbuckses { get; }

        public string Notes { get; }

        public decimal OtherAnnualIncome { get; }

        public ReadLookup PropertyType { get; }

        public ReadLookup PropertyStatus { get; }

        public decimal PurchasePrice { get; set; }

        public string ThumbnailImageBase64 { get; }

        public string ThumbnailImageContentType { get; }

        public int TotalUnits { get; }

        public int TotalUnitsSquareFootage { get; }

        public List<ReadUnitGroup> UnitGroups { get; }

        public int YearBuiltIn { get; }
    }
}