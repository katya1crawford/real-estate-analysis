using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateAnalysis.Data.Entities.Lookups;

namespace RealEstateAnalysis.Data.Entities.RentalProperty
{
    public class ClosingCost : BaseEntity
    {
        #region Constructors

        internal ClosingCost()
        {
        }

        public ClosingCost(decimal amount, ClosingCostType closingCostType)
        {
            Amount = Math.Round(amount, 2);
            ClosingCostType = closingCostType;
        }

        #endregion Constructors

        #region Properties

        public decimal Amount { get; private set; }

        internal long ClosingCostTypeId { get; private set; }

        public ClosingCostType ClosingCostType { get; private set; }

        internal long PropertyId { get; private set; }

        internal Property Property { get; private set; }

        #endregion Properties

        #region Commands

        public void Update(decimal amount)
        {
            Amount = Math.Round(amount, 2);
        }

        #endregion Commands
    }

    internal class ClosingCostConfiguration : IEntityTypeConfiguration<ClosingCost>
    {
        public void Configure(EntityTypeBuilder<ClosingCost> entity)
        {
            //Primary Key
            entity.HasKey(x => x.Id);

            //Relationships
            entity.HasOne(x => x.Property)
                .WithMany(x => x.ClosingCosts)
                .HasForeignKey(x => x.PropertyId)
                .IsRequired();

            entity.HasOne(x => x.ClosingCostType)
                .WithMany()
                .HasForeignKey(x => x.ClosingCostTypeId)
                .IsRequired();

            //Table & Columns
            entity.ToTable("ClosingCosts", "RentalProperty");
        }
    }
}