using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstateAnalysis.Data.Entities.RentalProperty
{
    public class UnitGroup : BaseEntity
    {
        #region Constructors

        internal UnitGroup()
        {
        }

        public UnitGroup(int numberOfUnits, int bedrooms, double bathrooms, int squareFootage)
        {
            Update(numberOfUnits: numberOfUnits,
                bedrooms: bedrooms,
                bathrooms: bathrooms,
                squareFootage: squareFootage);
        }

        #endregion Constructors

        #region Properties

        public double Bathrooms { get; private set; }

        public int Bedrooms { get; private set; }

        public int NumberOfUnits { get; private set; }

        public int SquareFootage { get; private set; }

        internal long PropertyId { get; private set; }

        internal Property Property { get; private set; }

        #endregion Properties

        #region Commands

        public void Update(int numberOfUnits, int bedrooms, double bathrooms, int squareFootage)
        {
            NumberOfUnits = numberOfUnits;
            Bedrooms = bedrooms;
            Bathrooms = bathrooms;
            SquareFootage = squareFootage;
        }

        #endregion Commands
    }

    internal class UnitGroupConfiguration : IEntityTypeConfiguration<UnitGroup>
    {
        public void Configure(EntityTypeBuilder<UnitGroup> entity)
        {
            //Primary Key
            entity.HasKey(x => x.Id);

            //Relationships
            entity.HasOne(x => x.Property)
                .WithMany(x => x.UnitGroups)
                .HasForeignKey(x => x.PropertyId)
                .IsRequired();

            //Table & Columns
            entity.ToTable("UnitGroups", "RentalProperty");
        }
    }
}