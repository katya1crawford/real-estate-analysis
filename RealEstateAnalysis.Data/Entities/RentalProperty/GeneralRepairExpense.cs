using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateAnalysis.Data.Entities.Lookups;

namespace RealEstateAnalysis.Data.Entities.RentalProperty
{
    public class GeneralRepairExpense : BaseEntity
    {
        #region Constructors

        internal GeneralRepairExpense()
        {
        }

        public GeneralRepairExpense(decimal amount, GeneralRepairExpenseType generalRepairExpenseType)
        {
            Amount = Math.Round(amount, 2);
            GeneralRepairExpenseType = generalRepairExpenseType;
        }

        #endregion Constructors

        #region Properties

        public decimal Amount { get; private set; }

        internal long GeneralRepairExpenseTypeId { get; private set; }

        public GeneralRepairExpenseType GeneralRepairExpenseType { get; private set; }

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

    internal class GeneralRepairExpenseConfiguration : IEntityTypeConfiguration<GeneralRepairExpense>
    {
        public void Configure(EntityTypeBuilder<GeneralRepairExpense> entity)
        {
            //Primary Key
            entity.HasKey(x => x.Id);

            //Relationships
            entity.HasOne(x => x.Property)
                .WithMany(x => x.GeneralRepairExpenses)
                .HasForeignKey(x => x.PropertyId)
                .IsRequired();

            entity.HasOne(x => x.GeneralRepairExpenseType)
                .WithMany()
                .HasForeignKey(x => x.GeneralRepairExpenseTypeId)
                .IsRequired();

            //Table & Columns
            entity.ToTable("GeneralRepairExpenses", "RentalProperty");
        }
    }
}