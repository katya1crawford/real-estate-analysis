using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstateAnalysis.Data.Entities.RentalProperty
{
    public class File : BaseEntity
    {
        #region Constructors

        internal File()
        {
        }

        public File(string name, string contentType, byte[] content)
        {
            Name = name;
            ContentType = contentType;
            FileContent = new FileContent(content: content);
        }

        #endregion Constructors

        #region Properties

        public string ContentType { get; private set; }

        public string Name { get; private set; }

        internal long PropertyId { get; private set; }

        internal Property Property { get; private set; }

        internal FileContent FileContent { get; private set; }

        #endregion Properties
    }

    internal class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> entity)
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

            //Relationships
            entity.HasOne(x => x.Property)
                .WithMany(x => x.Files)
                .HasForeignKey(x => x.PropertyId)
                .IsRequired();

            //Table & Columns
            entity.ToTable("Files", "RentalProperty");
        }
    }
}