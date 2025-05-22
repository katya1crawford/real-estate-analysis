using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateAnalysis.Data.Entities.Lookups;

namespace RealEstateAnalysis.Data.Entities.RentalProperty
{
    public class InteriorRepairExpense : BaseEntity
    {
        #region Constructors

        internal InteriorRepairExpense()
        {
        }

        public InteriorRepairExpense(decimal amount, InteriorRepairExpenseType interiorRepairExpenseType)
        {
            Amount = Math.Round(amount, 2);
            InteriorRepairExpenseType = interiorRepairExpenseType;
        }

        #endregion Constructors

        #region Properties

        public decimal Amount { get; private set; }

        internal long InteriorRepairExpenseTypeId { get; private set; }

        public InteriorRepairExpenseType InteriorRepairExpenseType { get; private set; }

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

    internal class InteriorRepairExpenseConfiguration : IEntityTypeConfiguration<InteriorRepairExpense>
    {
        public void Configure(EntityTypeBuilder<InteriorRepairExpense> entity)
        {
            //Primary Key
            entity.HasKey(x => x.Id);

            //Relationships
            entity.HasOne(x => x.Property)
                .WithMany(x => x.InteriorRepairExpenses)
                .HasForeignKey(x => x.PropertyId)
                .IsRequired();

            entity.HasOne(x => x.InteriorRepairExpenseType)
                .WithMany()
                .HasForeignKey(x => x.InteriorRepairExpenseTypeId)
                .IsRequired();

            //Table & Columns
            entity.ToTable("InteriorRepairExpenses", "RentalProperty");
        }
    }
}