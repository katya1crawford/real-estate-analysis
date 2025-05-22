using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateAnalysis.Data.Entities.Lookups;

namespace RealEstateAnalysis.Data.Entities.RentalProperty
{
    public class Property : BaseEntity
    {
        #region Constructors

        internal Property()
        {
            AnnualOperatingExpenses = new List<AnnualOperatingExpense>();
            ClosingCosts = new List<ClosingCost>();
            InteriorRepairExpenses = new List<InteriorRepairExpense>();
            ExteriorRepairExpenses = new List<ExteriorRepairExpense>();
            GeneralRepairExpenses = new List<GeneralRepairExpense>();
            Files = new List<File>();
            RentRollItems = new List<RentRollItem>();
        }

        public Property(string address,
           string city,
           State state,
           string zipCode,
           double latitude,
           double longitude,
           string neighborhood,
           string county,
           PropertyType propertyType,
           PropertyStatus propertyStatus,
           int yearBuiltIn,
           int buildingSquareFootage,
           int lotSquareFootage,
           decimal purchasePrice,
           decimal downPayment,
           decimal annualGrossScheduledRentalIncome,
           decimal otherAnnualIncome,
           decimal annualVacancyRate,
           decimal annualPropertyManagementFeeRate,
           decimal loanApr,
           int loanYears,
           string notes,
           List<UnitGroup> unitGroups,
           List<ClosingCost> closingCosts,
           List<AnnualOperatingExpense> annualOperatingExpenses,
           List<InteriorRepairExpense> interiorRepairExpenses,
           List<ExteriorRepairExpense> exteriorRepairExpenses,
           List<GeneralRepairExpense> generalRepairExpenses,
           decimal annualGrossScheduledRentalIncomeGrowthRate,
           decimal annualOperatingExpensesGrowthRate,
           decimal marketCapitalizationRate,
           byte[] thumbnailImage,
           string thumbnailImageContentType,
           string userId,
           string reportGroupName) : this()
        {
            Address = address;
            City = city;
            State = state;
            ZipCode = zipCode;
            Latitude = latitude;
            Longitude = longitude;
            Neighborhood = neighborhood;
            County = county;
            PropertyType = propertyType;
            PropertyStatus = propertyStatus;
            YearBuiltIn = yearBuiltIn;
            BuildingSquareFootage = buildingSquareFootage;
            LotSquareFootage = lotSquareFootage;
            PurchasePrice = purchasePrice;
            DownPayment = downPayment;
            AnnualGrossScheduledRentalIncome = annualGrossScheduledRentalIncome;
            OtherAnnualIncome = otherAnnualIncome;
            AnnualVacancyRate = annualVacancyRate;
            AnnualPropertyManagementFeeRate = annualPropertyManagementFeeRate;
            LoanApr = loanApr;
            LoanYears = loanYears;
            Notes = notes;
            UnitGroups = unitGroups;
            ClosingCosts = closingCosts;
            AnnualOperatingExpenses = annualOperatingExpenses;
            InteriorRepairExpenses = interiorRepairExpenses;
            ExteriorRepairExpenses = exteriorRepairExpenses;
            GeneralRepairExpenses = generalRepairExpenses;
            AnnualGrossScheduledRentalIncomeGrowthRate = annualGrossScheduledRentalIncomeGrowthRate;
            AnnualOperatingExpensesGrowthRate = annualOperatingExpensesGrowthRate;
            MarketCapitalizationRate = marketCapitalizationRate;
            ThumbnailImage = thumbnailImage;
            ThumbnailImageContentType = thumbnailImageContentType;
            UserId = userId;
            GroupName = reportGroupName;
        }

        #endregion Constructors

        #region Properties

        public string Address { get; private set; }

        public string City { get; private set; }

        public string ZipCode { get; private set; }

        public double Latitude { get; private set; }

        public double Longitude { get; private set; }

        public string Neighborhood { get; private set; }

        public string County { get; private set; }

        public int YearBuiltIn { get; private set; }

        public int BuildingSquareFootage { get; private set; }

        public int LotSquareFootage { get; private set; }

        public decimal PurchasePrice { get; private set; }

        public decimal DownPayment { get; private set; }

        public decimal AnnualGrossScheduledRentalIncome { get; private set; }

        public decimal OtherAnnualIncome { get; private set; }

        public decimal AnnualVacancyRate { get; private set; }

        public decimal AnnualPropertyManagementFeeRate { get; private set; }

        public decimal LoanApr { get; private set; }

        public int LoanYears { get; private set; }

        public string Notes { get; private set; }

        public byte[] ThumbnailImage { get; private set; }

        public string ThumbnailImageContentType { get; private set; }

        public decimal AnnualGrossScheduledRentalIncomeGrowthRate { get; private set; }

        public decimal AnnualOperatingExpensesGrowthRate { get; private set; }

        public decimal MarketCapitalizationRate { get; private set; }

        public string GroupName { get; private set; }

        public List<UnitGroup> UnitGroups { get; private set; }

        public List<ClosingCost> ClosingCosts { get; private set; }

        public List<AnnualOperatingExpense> AnnualOperatingExpenses { get; private set; }

        public List<InteriorRepairExpense> InteriorRepairExpenses { get; private set; }

        public List<ExteriorRepairExpense> ExteriorRepairExpenses { get; private set; }

        public List<GeneralRepairExpense> GeneralRepairExpenses { get; private set; }

        public List<File> Files { get; private set; }

        public List<RentRollItem> RentRollItems { get; private set; }

        internal long PropertyTypeId { get; private set; }

        public PropertyType PropertyType { get; private set; }

        internal long PropertyStatusId { get; private set; }

        public PropertyStatus PropertyStatus { get; private set; }

        internal long StateId { get; private set; }

        public State State { get; private set; }

        internal string UserId { get; private set; }

        internal User User { get; private set; }

        #endregion Properties

        #region Commands

        public void Update(string address,
           string city,
           State state,
           string zipCode,
           double latitude,
           double longitude,
           string neighborhood,
           string county,
           PropertyType propertyType,
           PropertyStatus propertyStatus,
           int yearBuiltIn,
           int buildingSquareFootage,
           int lotSquareFootage,
           decimal purchasePrice,
           decimal downPayment,
           decimal annualGrossScheduledRentalIncome,
           decimal otherAnnualIncome,
           decimal annualVacancyRate,
           decimal annualPropertyManagementFeeRate,
           decimal loanApr,
           int loanYears,
           string notes,
           decimal annualGrossScheduledRentalIncomeGrowthRate,
           decimal annualOperatingExpensesGrowthRate,
           decimal marketCapitalizationRate,
           byte[] thumbnailImage,
           string thumbnailImageContentType,
           string reportGroupName)
        {
            Address = address;
            City = city;
            State = state;
            ZipCode = zipCode;
            Latitude = latitude;
            Longitude = longitude;
            Neighborhood = neighborhood;
            County = county;
            PropertyType = propertyType;
            PropertyStatus = propertyStatus;
            YearBuiltIn = yearBuiltIn;
            BuildingSquareFootage = buildingSquareFootage;
            LotSquareFootage = lotSquareFootage;
            PurchasePrice = purchasePrice;
            DownPayment = downPayment;
            AnnualGrossScheduledRentalIncome = annualGrossScheduledRentalIncome;
            OtherAnnualIncome = otherAnnualIncome;
            AnnualVacancyRate = annualVacancyRate;
            AnnualPropertyManagementFeeRate = annualPropertyManagementFeeRate;
            LoanApr = loanApr;
            LoanYears = loanYears;
            Notes = notes;
            AnnualGrossScheduledRentalIncomeGrowthRate = annualGrossScheduledRentalIncomeGrowthRate;
            AnnualOperatingExpensesGrowthRate = annualOperatingExpensesGrowthRate;
            MarketCapitalizationRate = marketCapitalizationRate;
            ThumbnailImage = thumbnailImage;
            ThumbnailImageContentType = thumbnailImageContentType;
            GroupName = reportGroupName;
        }

        public void RemoveThumbnailImage()
        {
            ThumbnailImage = default;
            ThumbnailImageContentType = default;
        }

        public void AddFiles(List<File> files) =>
            Files.AddRange(files);

        public void RemoveFile(long fileId) =>
            Files.RemoveAll(x => x.Id == fileId);

        public void AddUnitGroup(UnitGroup unitGroup) =>
            UnitGroups.Add(unitGroup);

        public void RemoveUnitGroups(List<long> unitGorupIds) =>
            UnitGroups.RemoveAll(x => unitGorupIds.Contains(x.Id));

        public void AddAnnualOperatingExpense(AnnualOperatingExpense annualOperatingExpense) =>
            AnnualOperatingExpenses.Add(annualOperatingExpense);

        public void RemoveAnnualOperatingExpenses(List<long> annualOperatingExpenseIds) =>
           AnnualOperatingExpenses.RemoveAll(x => annualOperatingExpenseIds.Contains(x.Id));

        public void AddClosingCost(ClosingCost closingCosts) =>
            ClosingCosts.Add(closingCosts);

        public void RemoveClosingCosts(List<long> closingCostIds) =>
           ClosingCosts.RemoveAll(x => closingCostIds.Contains(x.Id));

        public void AddInteriorRepairExpense(InteriorRepairExpense interiorRepairExpense) =>
            InteriorRepairExpenses.Add(interiorRepairExpense);

        public void RemoveInteriorRepairExpenses(List<long> interiorRepairExpenseIds) =>
           InteriorRepairExpenses.RemoveAll(x => interiorRepairExpenseIds.Contains(x.Id));

        public void AddExteriorRepairExpense(ExteriorRepairExpense exteriorRepairExpense) =>
            ExteriorRepairExpenses.Add(exteriorRepairExpense);

        public void RemoveExteriorRepairExpenses(List<long> exteriorRepairExpenseIds) =>
           ExteriorRepairExpenses.RemoveAll(x => exteriorRepairExpenseIds.Contains(x.Id));

        public void AddGeneralRepairExpense(GeneralRepairExpense generalRepairExpense) =>
            GeneralRepairExpenses.Add(generalRepairExpense);

        public void RemoveGeneralRepairExpenses(List<long> generalRepairExpenseIds) =>
           GeneralRepairExpenses.RemoveAll(x => generalRepairExpenseIds.Contains(x.Id));

        public void AddRentRollItems(List<RentRollItem> rentRollItems)
        {
            RentRollItems.Clear();
            RentRollItems.AddRange(rentRollItems);
        }

        #endregion Commands
    }

    internal class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> entity)
        {
            //Primary Key
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.UserId)
                .HasMaxLength(450)
                .IsRequired();

            entity.Property(x => x.Address)
                .HasMaxLength(500)
                .IsRequired();

            entity.Property(x => x.City)
                .HasMaxLength(500)
                .IsRequired();

            entity.Property(x => x.Neighborhood)
                .HasMaxLength(250);

            entity.Property(x => x.County)
                .HasMaxLength(250);

            entity.Property(x => x.AnnualVacancyRate)
                .HasColumnType("decimal(5, 2)");

            entity.Property(x => x.AnnualPropertyManagementFeeRate)
                .HasColumnType("decimal(5, 2)");

            entity.Property(x => x.LoanApr)
                .HasColumnType("decimal(5, 2)");

            entity.Property(x => x.AnnualGrossScheduledRentalIncomeGrowthRate)
                .HasColumnType("decimal(5, 2)");

            entity.Property(x => x.MarketCapitalizationRate)
                .HasColumnType("decimal(5, 2)");

            entity.Property(x => x.ZipCode)
                .HasMaxLength(10)
                .IsRequired();

            entity.Property(x => x.GroupName)
                .HasMaxLength(50);

            //Relationships
            entity.HasOne(x => x.State)
                .WithMany()
                .HasForeignKey(x => x.StateId)
                .IsRequired();

            entity.HasOne(x => x.PropertyType)
                .WithMany()
                .HasForeignKey(x => x.PropertyTypeId)
                .IsRequired();

            entity.HasOne(x => x.PropertyStatus)
                .WithMany()
                .HasForeignKey(x => x.PropertyStatusId)
                .IsRequired();

            entity.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            entity.HasMany(x => x.RentRollItems)
                .WithOne()
                .IsRequired();

            //Table & Columns
            entity.ToTable("Properties", "RentalProperty");
        }
    }
}