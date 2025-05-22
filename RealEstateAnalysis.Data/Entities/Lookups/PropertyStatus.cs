using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstateAnalysis.Data.Entities.Lookups
{
    public class PropertyStatus
    {
        public long Id { get; private set; }

        public string Name { get; private set; }
    }

    internal class PropertyStatusConfiguration : IEntityTypeConfiguration<PropertyStatus>
    {
        public void Configure(EntityTypeBuilder<PropertyStatus> entity)
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
            entity.ToTable("PropertyStatuses", "Lookup");
        }
    }
}