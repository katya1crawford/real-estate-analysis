using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstateAnalysis.Data.Entities.LocationAnalysis
{
    public class CityDataStateUrl
    {
        public long Id { get; private set; }

        public string Url { get; private set; }
    }

    internal class CityDataStateUrlConfiguration : IEntityTypeConfiguration<CityDataStateUrl>
    {
        public void Configure(EntityTypeBuilder<CityDataStateUrl> builder)
        {
            //Primary Key
            builder.HasKey(x => x.Id);

            //Properties
            builder.Property(x => x.Url)
                .HasMaxLength(500)
                .IsRequired();

            //Table & Columns
            builder.ToTable("CityDataStateUrls", "LocationAnalysis");
        }
    }
}