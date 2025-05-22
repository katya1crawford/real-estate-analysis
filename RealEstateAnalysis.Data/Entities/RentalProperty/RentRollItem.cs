using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstateAnalysis.Data.Entities.RentalProperty
{
    public class RentRollItem : BaseEntity
    {
        public RentRollItem(string unit, int squareFootage, int bedrooms, double bathrooms, bool isVacant, bool isRenovated, decimal? contractRent, decimal? otherIncome,
            decimal? marketRent, DateTime? leaseStartDate, DateTime? leaseEndDate)
        {
            Unit = unit;
            SquareFootage = squareFootage;
            Bedrooms = bedrooms;
            Bathrooms = bathrooms;
            IsVacant = isVacant;
            IsRenovated = isRenovated;
            ContractRent = contractRent;
            OtherIncome = otherIncome;
            MarketRent = marketRent;
            LeaseStartDate = leaseStartDate;
            LeaseEndDate = leaseEndDate;
        }

        public string Unit { get; private set; }

        public int SquareFootage { get; private set; }

        public int Bedrooms { get; private set; }

        public double Bathrooms { get; private set; }

        public bool IsVacant { get; private set; }

        public bool IsRenovated { get; private set; }

        public decimal? ContractRent { get; private set; }

        public decimal? OtherIncome { get; private set; }

        public decimal? MarketRent { get; private set; }

        public DateTime? LeaseStartDate { get; private set; }

        public DateTime? LeaseEndDate { get; private set; }
    }

    internal class RentRollItemConfiguration : IEntityTypeConfiguration<RentRollItem>
    {
        public void Configure(EntityTypeBuilder<RentRollItem> builder)
        {
            //Primary Key
            builder.HasKey(x => x.Id);

            //Properties
            builder.Property(x => x.Unit)
                .HasMaxLength(250)
                .IsRequired();

            //Table & Columns
            builder.ToTable("RentRollItems", "RentalProperty");
        }
    }
}