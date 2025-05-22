using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstateAnalysis.Data.Entities.Lookups
{
    public class InteriorRepairExpenseType
    {
        public long Id { get; private set; }

        public string Name { get; private set; }
    }

    internal class InteriorRepairExpenseTypeConfiguration : IEntityTypeConfiguration<InteriorRepairExpenseType>
    {
        public void Configure(EntityTypeBuilder<InteriorRepairExpenseType> entity)
        {
            //Primary Key
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.Id)
                .ValueGeneratedNever();

            entity.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            //Table & Columns
            entity.ToTable("InteriorRepairExpenseTypes", "Lookup");
        }
    }
}