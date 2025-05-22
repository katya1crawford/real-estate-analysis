using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateAnalysis.Data.Entities.Lookups;

namespace RealEstateAnalysis.Data.Entities.Reonomy
{
    public class SoldProperty : BaseEntity
    {
        #region Constructors

        internal SoldProperty()
        {
        }

        public SoldProperty(string address,
            string city,
            string zipCode,
            int? buildingSquareFootage,
            string fips,
            string sourceId,
            int? lotSquareFootage,
            decimal? mortgageAmount,
            string mortgageLenderName,
            DateTime? mortgageRecordingDate,
            DateTime? salesDate,
            string stdLandUseCodeDescription,
            int? totalUnits,
            int? yearBuilt,
            decimal? salesPrice,
            State state,
            PropertyType propertyType)
        {
            Address = address;
            City = city;
            ZipCode = zipCode;
            BuildingSquareFootage = buildingSquareFootage;
            Fips = fips;
            SourceId = sourceId;
            LotSquareFootage = lotSquareFootage;
            MortgageAmount = mortgageAmount;
            MortgageLenderName = mortgageLenderName;
            MortgageRecordingDate = mortgageRecordingDate;
            SalesDate = salesDate;
            StdLandUseCodeDescription = stdLandUseCodeDescription;
            TotalUnits = totalUnits;
            YearBuilt = yearBuilt;
            SalesPrice = salesPrice;
            State = state;
            PropertyType = propertyType;
        }

        #endregion Constructors

        #region Properties

        public string Address { get; private set; }

        public string City { get; private set; }

        public string ZipCode { get; private set; }

        public string Neighborhood { get; private set; }

        public string County { get; private set; }

        public int? BuildingSquareFootage { get; private set; }

        public string Fips { get; private set; }

        public string SourceId { get; private set; }

        public int? LotSquareFootage { get; private set; }

        public decimal? MortgageAmount { get; private set; }

        public string MortgageLenderName { get; private set; }

        public DateTime? MortgageRecordingDate { get; private set; }

        public DateTime? SalesDate { get; private set; }

        public string StdLandUseCodeDescription { get; private set; }

        public int? TotalUnits { get; private set; }

        public int? YearBuilt { get; private set; }

        public decimal? SalesPrice { get; private set; }

        public double? Latitude { get; private set; }

        public double? Longitude { get; private set; }

        public bool InvalidAddress { get; private set; }

        internal long StateId { get; private set; }

        public State State { get; private set; }

        internal long PropertyTypeId { get; private set; }

        public PropertyType PropertyType { get; private set; }

        #endregion Properties

        #region Commands

        public void UpdateAddressParts(double latitude, double longitude, string neighborhood, string county)
        {
            Latitude = latitude;
            Longitude = longitude;
            Neighborhood = neighborhood;
            County = county;
        }

        public void MarkAddresInvalid()
        {
            InvalidAddress = true;
        }

        #endregion Commands
    }

    internal class SoldPropertyConfiguration : IEntityTypeConfiguration<SoldProperty>
    {
        public void Configure(EntityTypeBuilder<SoldProperty> entity)
        {
            //Primary Key
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.Address)
                .HasMaxLength(500);

            entity.Property(x => x.City)
                .HasMaxLength(500);

            entity.Property(x => x.ZipCode)
                .HasMaxLength(10);

            entity.Property(x => x.Neighborhood)
                .HasMaxLength(250);

            entity.Property(x => x.County)
                .HasMaxLength(250);

            entity.Property(x => x.Fips)
                .HasMaxLength(250);

            entity.Property(x => x.SourceId)
                .HasMaxLength(100);

            entity.Property(x => x.MortgageLenderName)
                .HasMaxLength(150);

            entity.Property(x => x.StdLandUseCodeDescription)
                .HasMaxLength(500);

            //Relationships
            entity.HasOne(x => x.State)
                .WithMany()
                .HasForeignKey(x => x.StateId)
                .IsRequired();

            entity.HasOne(x => x.PropertyType)
                .WithMany()
                .HasForeignKey(x => x.PropertyTypeId)
                .IsRequired();

            //Table & Columns
            entity.ToTable("SoldProperties", "Reonomy");
        }
    }
}