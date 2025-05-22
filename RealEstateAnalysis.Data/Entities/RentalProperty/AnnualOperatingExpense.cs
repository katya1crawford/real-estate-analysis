using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateAnalysis.Data.Entities.Lookups;

namespace RealEstateAnalysis.Data.Entities.RentalProperty
{
    public class AnnualOperatingExpense : BaseEntity
    {
        #region Constructors

        internal AnnualOperatingExpense()
        {
        }

        public AnnualOperatingExpense(decimal amount, OperatingExpenseType operatingExpenseType)
        {
            Amount = Math.Round(amount, 2);
            OperatingExpenseType = operatingExpenseType;
        }

        #endregion Constructors

        #region Properties

        public decimal Amount { get; private set; }

        internal long OperatingExpenseTypeId { get; private set; }

        public OperatingExpenseType OperatingExpenseType { get; private set; }

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

    internal class AnnualOperatingExpenseConfiguration : IEntityTypeConfiguration<AnnualOperatingExpense>
    {
        public void Configure(EntityTypeBuilder<AnnualOperatingExpense> entity)
        {
            //Primary Key
            entity.HasKey(x => x.Id);

            //Relationships
            entity.HasOne(x => x.Property)
                .WithMany(x => x.AnnualOperatingExpenses)
                .HasForeignKey(x => x.PropertyId)
                .IsRequired();

            entity.HasOne(x => x.OperatingExpenseType)
                .WithMany()
                .HasForeignKey(x => x.OperatingExpenseTypeId)
                .IsRequired();

            //Table & Columns
            entity.ToTable("AnnualOperatingExpenses", "RentalProperty");
        }
    }
}