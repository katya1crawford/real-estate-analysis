using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstateAnalysis.Data.Entities.RentalProperty
{
    public class FileContent
    {
        #region Constructors

        internal FileContent()
        {
        }

        internal FileContent(byte[] content)
        {
            Content = content;
        }

        #endregion Constructors

        #region Properties

        internal long Id { get; private set; }

        public byte[] Content { get; private set; }

        internal long FileId { get; private set; }

        internal File File { get; private set; }

        #endregion Properties
    }

    internal class FileContentConfiguration : IEntityTypeConfiguration<FileContent>
    {
        public void Configure(EntityTypeBuilder<FileContent> entity)
        {
            //Primary Key
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.Content)
                .IsRequired();

            //Relationships
            entity.HasOne(x => x.File)
                .WithOne(x => x.FileContent)
                .HasForeignKey<FileContent>(x => x.FileId)
                .OnDelete(DeleteBehavior.Cascade);

            //Table & Columns
            entity.ToTable("FilesContent", "RentalProperty");
        }
    }
}