using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateAnalysis.Data.Entities.Lookups;

namespace RealEstateAnalysis.Data.Entities.RentalProperty
{
    public class ExteriorRepairExpense : BaseEntity
    {
        #region Constructors

        internal ExteriorRepairExpense()
        {
        }

        public ExteriorRepairExpense(decimal amount, ExteriorRepairExpenseType exteriorRepairExpenseType)
        {
            Amount = Math.Round(amount, 2);
            ExteriorRepairExpenseType = exteriorRepairExpenseType;
        }

        #endregion Constructors

        #region Properties

        public decimal Amount { get; private set; }

        internal long ExteriorRepairItemTypeId { get; private set; }

        public ExteriorRepairExpenseType ExteriorRepairExpenseType { get; private set; }

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

    internal class ExteriorRepairExpenseConfiguration : IEntityTypeConfiguration<ExteriorRepairExpense>
    {
        public void Configure(EntityTypeBuilder<ExteriorRepairExpense> entity)
        {
            //Primary Key
            entity.HasKey(x => x.Id);

            //Relationships
            entity.HasOne(x => x.Property)
                .WithMany(x => x.ExteriorRepairExpenses)
                .HasForeignKey(x => x.PropertyId)
                .IsRequired();

            entity.HasOne(x => x.ExteriorRepairExpenseType)
                .WithMany()
                .HasForeignKey(x => x.ExteriorRepairItemTypeId)
                .IsRequired();

            //Table & Columns
            entity.ToTable("ExteriorRepairExpenses", "RentalProperty");
        }
    }
}