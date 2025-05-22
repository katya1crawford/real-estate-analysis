using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstateAnalysis.Data.Entities.RentalProperty
{
    public class GalleryImage : BaseEntity
    {
        internal GalleryImage()
        {
        }

        public GalleryImage(string name, string contentType, byte[] content, long propertyId)
        {
            Name = name;
            ContentType = contentType;
            Content = content;
            PropertyId = propertyId;
        }

        public string ContentType { get; private set; }

        public string Name { get; private set; }

        public byte[] Content { get; private set; }

        internal long PropertyId { get; private set; }

        internal Property Property { get; private set; }
    }

    internal class GalleryImageConfiguration : IEntityTypeConfiguration<GalleryImage>
    {
        public void Configure(EntityTypeBuilder<GalleryImage> entity)
        {
            //Ignore
            entity.Ignore(x => x.ModifiedDate);

            //Primary Key
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.ContentType)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.Content)
                .IsRequired();

            //Relationships
            entity.HasOne(x => x.Property)
                .WithMany()
                .HasForeignKey(x => x.PropertyId)
                .IsRequired();

            //Table & Columns
            entity.ToTable("GalleryImages", "RentalProperty");
        }
    }
}